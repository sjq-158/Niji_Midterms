namespace Niji_Midterms.Models;

public class ScheduleEntry
{
    public int Id { get; set; }
    public DayOfWeek Day { get; set; }
    public int TitleId { get; set; }
    public string Episode { get; set; } = "";
}