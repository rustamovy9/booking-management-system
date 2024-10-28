using System.Runtime.InteropServices.JavaScript;

namespace MainApp.Filter;

public class BookingFilter:BaseFilter
{
    public DateTime? StartDateTime { get; set; }
    public DateTime? EndDateTime { get; set; }
}