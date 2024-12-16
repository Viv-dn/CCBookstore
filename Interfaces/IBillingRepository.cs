using CCBookstore.Models;

namespace CCBookstore.Interfaces;

public interface IBillingRepository
{
    Task<List<Billing>> GetAllBillingsAsync();
    Task<Billing?> GetBillingsByIdAsync(int id);
    Task CreateBillingAsync(Billing newBilling);
}