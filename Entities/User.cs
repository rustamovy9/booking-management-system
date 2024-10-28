namespace MainApp.Entities;

public class User:BaseEntity
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public ICollection<Booking> Bookings { get; set; } = [];
}