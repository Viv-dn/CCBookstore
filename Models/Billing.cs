namespace CCBookstore.Models;

public class Billing
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int UserId { get; set; }
    public double TotalSum { get; set; }
}