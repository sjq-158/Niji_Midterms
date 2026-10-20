using Niji_Midterms.Models;

namespace Niji_Midterms.Services;

public class CatalogService
{
    private static readonly string[] Photos =
    [
        "photo-1607604276583-eef5d076aa5f", "photo-1563089145-599997674d42",
        "photo-1518709268805-4e9042af9f23", "photo-1534447677768-be436bb09401",
        "photo-1579783902614-a3fb3927b675", "photo-1578632767115-351597cf2477"
    ];

    public static string Img(int n, int w = 600) =>
        $"https://images.unsplash.com/{Photos[n % Photos.Length]}?auto=format&fit=crop&w={w}&q=80";

    private readonly List<Title> _titles;
    private readonly List<ScheduleEntry> _schedule = [];

    public CatalogService()
    {
        _titles =
        [
            T(1, "Solo Leveling", "Action · Fantasy", TitleKind.Comic, "NEW", "Rise from the weakest hunter to the sovereign of shadows."),
            T(2, "Omniscient Reader", "Apocalypse · Fantasy", TitleKind.Comic, "UP", "The world has ended, and a new novel has begun."),
            T(3, "The S-Classes That I Raised", "Action · Comedy", TitleKind.Comic, "HIT", "Raising top-tier hunters from ordinary people."),
            T(4, "Trash of the Count's Family", "Fantasy · Isekai", TitleKind.Comic, "HOT", "I just want to live a peaceful life here."),
            T(5, "Under the Oak Tree", "Romance · Fantasy", TitleKind.Comic, "NEW"),
            T(6, "Finding Camellia", "Romance · Drama", TitleKind.Comic, "HIT"),
            T(7, "The Greatest Estate Developer", "Comedy · Fantasy", TitleKind.Comic, "UP"),
            T(8, "Sss-Class Revival Hunter", "Action · Fantasy", TitleKind.Comic, "NEW"),
            T(9, "The Duke's Maid", "Romance · Isekai", TitleKind.Comic, "HIT"),
            T(10, "How to Hide the Emperor's Child", "Romance · Drama", TitleKind.Comic, "TOP"),

            T(11, "Love History Caused by Willful Negligence", "Modern Romance", TitleKind.Novel, "EVENT", "Light novel · Free Pass"),
            T(12, "Solstice: Book of Secrets", "Mystery Romance", TitleKind.Novel, "EVENT", "She came for secrets. He came for her."),
            T(13, "Your Highness, It's Just a Novel", "Royal Fantasy", TitleKind.Novel, "10% OFF", "From paper to bedsheets."),
            T(14, "Betrayal of Dignity", "Romance Special", TitleKind.Novel, "HOT"),
            T(15, "High Society", "Drama Edition", TitleKind.Novel, "NEW"),
            T(16, "Semantic Error", "BL Light Novel", TitleKind.Novel, "HIT"),
        ];

        int id = 1;
        var comics = GetTitles(TitleKind.Comic);
        foreach (var day in Enum.GetValues<DayOfWeek>())
            for (int i = 0; i < 4; i++)
            {
                _schedule.Add(new ScheduleEntry
                {
                    Id = id,
                    Day = day,
                    TitleId = comics[((int)day * 3 + i) % comics.Count].Id,
                    Episode = $"Chapter {20 + id}"
                });
                id++;
            }
    }

    private static Title T(int id, string name, string genre, TitleKind kind, string badge, string? tagline = null) =>
        new() { Id = id, Name = name, Author = "niji Original", Genre = genre, Kind = kind, Badge = badge, Tagline = tagline, ImageUrl = Img(id) };

    public IReadOnlyList<Title> GetTitles(TitleKind kind) => _titles.Where(t => t.Kind == kind).ToList();

    public IReadOnlyList<Title> GetTrending(int take = 10) => GetTitles(TitleKind.Comic).Take(take).ToList();

    public IReadOnlyList<(ScheduleEntry Entry, Title Title)> GetSchedule(DayOfWeek day) =>
        _schedule.Where(s => s.Day == day)
                 .Select(s => (s, _titles.First(t => t.Id == s.TitleId)))
                 .ToList();
}