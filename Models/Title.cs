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
    public string Photo { get; set; } = "";

    public string ImageUrl => Image(600);
    public string Byline => string.IsNullOrWhiteSpace(Genre) ? Author : $"{Author} / {Genre}";

    public string Image(int width) => Url(Photo, width);

    public static string Url(string photo, int width) =>
        $"https://images.unsplash.com/{photo}?auto=format&fit=crop&w={width}&q=80";
}