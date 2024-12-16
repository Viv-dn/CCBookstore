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

app.UseSwagger();
app.UseSwaggerUI();
// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
