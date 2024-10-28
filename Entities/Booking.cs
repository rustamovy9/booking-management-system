namespace MainApp.Entities;

public class Booking:BaseEntity 
{
    public int UserId { get; set; }
    public int ProviderId { get; set; }
    public int ServiceInfoId { get; set; }
    public DateTime StartDateTime { get; set; } 
    public DateTime EndDateTime { get; set; }
    public decimal TotalPrice { get; set; }

    public User User { get; set; }
    public Provider Provider { get; set; }
    public ServiceInfo ServiceInfo { get; set; }
}