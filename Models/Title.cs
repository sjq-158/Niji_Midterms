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

    // File name inside wwwroot/images/manhwas, e.g. "solo-leveling.jpg"
    public string Cover { get; set; } = "";

    public bool IsCompleted { get; set; }
    public int Volumes { get; set; }

    public string ImageUrl => Url(Cover);
    public string Byline => string.IsNullOrWhiteSpace(Genre) ? Author : $"{Author} / {Genre}";

    // "Action · Fantasy" -> ["Action", "Fantasy"]
    public IEnumerable<string> Genres =>
        Genre.Split('·', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

    public static string Url(string cover) => $"/images/manhwas/{cover}";
}