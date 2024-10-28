namespace MainApp.Filter;

public class ServiceInfoFilter:BaseFilter
{
    public string? Name { get; set; } = null!;
    public decimal? MaxPrice { get; set; }
    public decimal? MinPrice { get; set; }
    public string? Address { get; set; }
}