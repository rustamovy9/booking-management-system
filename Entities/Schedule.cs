namespace MainApp.Entities;

public class Schedule:BaseEntity
{
    public int ProviderId { get; set; }
    public DateTime ScheduleDate { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    public Provider Provider { get; set; } 
}