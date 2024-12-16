using CCBookstore.Interfaces;
using CCBookstore.Models;
using CCBookstore.Notifications;
using CCBookstore.Repositories;

namespace CCBookstore.Services;

public class BillingService
{
    private readonly BookstoreDbContext _context;
    private readonly ProductSubject _prodSub;

    public IBillingRepository Billings { get; private set; }

    public BillingService(BookstoreDbContext context, ProductSubject prodSub)
    {
        _context = context;
        Billings = new BillingRepository(context);

        _prodSub = prodSub;
        _prodSub.Attach(new EmailNotification());
    }

    public void NotifyBillingAdded(Billing billing)
    {
        _prodSub.Notify(billing);
    }
}