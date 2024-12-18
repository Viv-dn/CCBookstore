using CCBookstore;
using CCBookstore.Interfaces;
using CCBookstore.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("BillingDB");

builder.Services.AddDbContext<BookstoreDbContext>(
    options => options.UseSqlServer(connectionString)
);

builder.Services.AddScoped<IBillingRepository, BillingRepository>();
builder.Services.AddHttpClient();


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<BookstoreDbContext>();
    dbContext.Database.Migrate();
}
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();
    var dbContext = services.GetRequiredService<BookstoreDbContext>();

    var maxRetries = 5;
    var delayMilliseconds = 2000;
    var connected = false;

    for (int i = 0; i < maxRetries; i++)
    {
        try
        {
            logger.LogInformation("Försöker ansluta till databasen... Försök {Attempt}", i + 1);
            await dbContext.Database.OpenConnectionAsync();
            logger.LogInformation("Anslutning till databasen lyckades.");
            await dbContext.Database.CloseConnectionAsync();
            connected = true;
            break;
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "Misslyckades med att ansluta till databasen. Försöker igen om {Delay} ms...", delayMilliseconds);
            await Task.Delay(delayMilliseconds);
        }
    }

    if (!connected)
    {
        logger.LogError("Kunde inte ansluta till databasen efter {MaxRetries} försök.", maxRetries);
        throw new Exception("Kunde inte ansluta till databasen.");
    }

    // Utför eventuella databasoperationer, t.ex. migreringar
    dbContext.Database.Migrate();
}

app.UseSwagger();
app.UseSwaggerUI();
// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
