namespace MainApp.Filter;

public class ScheduleFilter:BaseFilter
{
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
}