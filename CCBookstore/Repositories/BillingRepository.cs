using CCBookstore.Interfaces;
using CCBookstore.Models;
using Microsoft.EntityFrameworkCore;

namespace CCBookstore.Repositories;

public class BillingRepository(BookstoreDbContext context) : IBillingRepository
{
    //public BillingRepository(BookstoreDbContext context) : base()
    //{
    //}

    public async Task<List<Billing>> GetAllBillingsAsync()
    {
        return await context.Billings.ToListAsync();
    }

    public async Task<Billing?> GetBillingsByIdAsync(int id)
    {
        return await context.Billings.FindAsync(id);
    }

    public async Task CreateBillingAsync(Billing newBilling)
    {
        var billing = new Billing()
        {
            Id = newBilling.Id,
            OrderId = newBilling.OrderId,
            UserId = newBilling.UserId,
            TotalSum = newBilling.TotalSum
        };
        await context.Billings.AddAsync(billing);
        await context.SaveChangesAsync();
    }
}