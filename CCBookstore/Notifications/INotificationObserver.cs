using CCBookstore.Models;

namespace CCBookstore.Notifications;

public interface INotificationObserver
{
    void UpdateNote(Billing billing);
}