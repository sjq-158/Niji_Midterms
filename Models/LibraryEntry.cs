namespace Niji_Midterms.Models;

public enum ReadingStatus { Reading, PlanToRead, Completed }

public class LibraryEntry
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int TitleId { get; set; }
    public ReadingStatus Status { get; set; } = ReadingStatus.PlanToRead;
    public DateTime AddedOn { get; set; } = DateTime.UtcNow;
}