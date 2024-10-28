namespace MainApp.Entities;

public class ServiceInfo:BaseEntity
{
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public string Address { get; set; } = null!;
    public ICollection<Booking> Bookings { get; set; } = [];
}