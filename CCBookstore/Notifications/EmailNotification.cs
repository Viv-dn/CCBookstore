using CCBookstore.Models;

namespace CCBookstore.Notifications;

public class EmailNotification : INotificationObserver
{
    public void UpdateNote(Billing billing)
    {
        //skicka mejl när en order har gjorts så att billing kan skickas 
        Console.WriteLine($"Email notification: An order has been placed. A billing with id: {billing.Id} has been created.");
    }
}