namespace Niji_Midterms.Models;

public enum TitleKind { Comic, Novel }

public class Title
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Author { get; set; } = "";
    public string Genre { get; set; } = "";
    public TitleKind Kind { get; set; }
    public string Badge { get; set; } = "";
    public string? Tagline { get; set; }
    public string ImageUrl { get; set; } = "";
}