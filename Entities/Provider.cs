namespace MainApp.Entities;

public class Provider:BaseEntity
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Specialization { get; set; } = null!;
    public string Address { get; set; } = null!;

    public ICollection<Schedule> Schedules { get; set; } = [];
    public ICollection<Booking> Bookings { get; set; } = [];
}