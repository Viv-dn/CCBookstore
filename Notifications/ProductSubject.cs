using CCBookstore.Models;

namespace CCBookstore.Notifications;

public class ProductSubject
{
    private readonly List<INotificationObserver> _observer = new List<INotificationObserver>();

    public void Attach(INotificationObserver observer)
    {
        _observer.Add(observer);
    }

    public void Detach(INotificationObserver observer)
    {
        _observer.Remove(observer);
    }

    public void Notify(Billing billing)
    {
        foreach (var observer in _observer)
        {
            observer.UpdateNote(billing);
        }
    }
}