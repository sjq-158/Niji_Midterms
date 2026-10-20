using Niji_Midterms.Models;

namespace Niji_Midterms.Services;

public static class Photos
{
    public const string Hunter  = "photo-1607604276583-eef5d076aa5f";
    public const string Reader  = "photo-1563089145-599997674d42";
    public const string Boxer   = "photo-1518709268805-4e9042af9f23";
    public const string Count   = "photo-1534447677768-be436bb09401";
    public const string OakTree = "photo-1511497584788-876761c11969";
    public const string Lookism = "photo-1578632767115-351597cf2477";
    public const string Banner  = "photo-1544716278-ca5e3f4abd8c";
}

public class CatalogService
{
    private readonly List<Title> _titles;
    private readonly List<ScheduleEntry> _schedule = [];
    private readonly List<SpotlightItem> _spotlight;

    public CatalogService()
    {
        _titles =
        [
            // Comics (ids 1-10, same order as Top Trending on home.html)
            C(1, "Solo Leveling", "Action · Fantasy", "NEW", Photos.Hunter, "Rise from the weakest hunter to the sovereign of shadows."),
            C(2, "Omniscient Reader", "Apocalypse · Fantasy", "UP", Photos.Reader, "The world has ended, and a new novel has begun."),
            C(3, "The Boxer", "Sports · Drama", "HIT", Photos.Boxer, "A journey beyond human limits in the ring."),
            C(4, "Trash of Count's Family", "Fantasy · Isekai", "HOT", Photos.Count, "I just want to live a peaceful life here."),
            C(5, "Under the Oak Tree", "Romance · Fantasy", "NEW", Photos.OakTree, "A timid noblewoman and a fierce knight in a grand romance."),
            C(6, "Lookism", "Drama · Action", "HIT", Photos.Lookism, "Two bodies, completely different daily lives."),
            C(7, "Eleceed", "Action · Supernatural", "UP", Photos.Boxer, "Awakened powers and secret agent cats."),
            C(8, "SSS-Class Hunter", "Action · Fantasy", "NEW", Photos.Reader),
            C(9, "Wind Breaker", "Sports · Action", "HIT", Photos.Hunter),
            C(10, "Sweet Home", "Horror · Thriller", "TOP", Photos.Count, "Desire monsters plague the apartment complex."),

            // Novels (ids 11-20, same order as Popular Webnovels on novel.html)
            N(11, "Under the Oak Tree", "Suji Kim", "Romance Novel", "BESTSELLER", Photos.OakTree, "Stuttering lady meets a hero from the crusade."),
            N(12, "Omniscient Reader", "Sing Shong", "Volume Edition", "EVENT", Photos.Reader, "The end of the world is only the beginning."),
            N(13, "Trash of Count's Family", "Yoo Ryeo Han", "Fantasy", "POPULAR", Photos.Count, "I just want to live a quiet life as a slacker."),
            N(14, "Solo Leveling", "Chugong", "Webnovel", "FREE PASS", Photos.Hunter, "Shadow monarch rises from dark dungeons."),
            N(15, "The Boxer", "JH", "Sports Drama", "COMPLETE", Photos.Boxer, "Gifted with overwhelming boxing prowess."),
            N(16, "Lookism", "Park Tae-jun", "", "NOVEL", Photos.Lookism),
            N(17, "Eleceed", "Zhena / Son Jeho", "", "NOVEL", Photos.Boxer),
            N(18, "SSS-Class Hunter", "Shin Noah", "", "EXCLUSIVE", Photos.Reader, "Dying to copy skills of the strongest."),
            N(19, "Wind Breaker", "Jo Yongseok", "", "NOVEL", Photos.Hunter),
            N(20, "Sweet Home", "Kim Carnby", "", "NOVEL", Photos.Count),
        ];

        _spotlight =
        [
            // comics.html spotlight (2 pages of 3)
            S(5, "Under the Oak Tree", "A timid noblewoman and a fierce knight in a grand romance.", "HIT", "ROMANCE"),
            S(3, "The Boxer", "A journey beyond human limits in the ring.", "EVENT", "SPORTS"),
            S(6, "Lookism", "Two bodies, completely different daily lives.", "EXCLUSIVE", "HIT"),
            S(7, "Eleceed", "Awakened powers and secret agent cats.", "ACTION", "SUPERPOWER"),
            S(10, "Sweet Home", "Desire monsters plague the apartment complex.", "THRILLER", "ORIGINAL"),
            S(1, "Solo Leveling", "The weakest hunter becomes sovereign.", "FANTASY", "TOP 10"),

            // novel.html showcase (2 pages of 3)
            S(11, "Under the Oak Tree", "Stuttering lady meets a hero from the crusade.", "BESTSELLER", "ROMANCE"),
            S(12, "Omniscient Reader's Viewpoint", "The end of the world is only the beginning.", "EVENT", "NOVEL"),
            S(14, "Solo Leveling", "Shadow monarch rises from dark dungeons.", "ACTION", "FREE PASS"),
            S(13, "Trash of Count's Family", "I just want to live a quiet life as a slacker.", "POPULAR", "NOVEL"),
            S(15, "The Boxer", "Gifted with overwhelming boxing prowess.", "DRAMA", "COMPLETE"),
            S(18, "SSS-Class Revival Hunter", "Dying to copy skills of the strongest.", "EXCLUSIVE", "NOVEL"),
        ];

        // home.html weekly schedule
        Add(DayOfWeek.Monday,    (1, "Chapter 150"), (2, "Chapter 110"),  (3, "Chapter 72"),   (5, "Chapter 54"));
        Add(DayOfWeek.Tuesday,   (5, "Chapter 54"),  (6, "Chapter 312"),  (7, "Chapter 145"),  (8, "Chapter 82"));
        Add(DayOfWeek.Wednesday, (9, "Chapter 288"), (10, "Chapter 120"), (1, "Chapter 151"),  (5, "Chapter 55"));
        Add(DayOfWeek.Thursday,  (3, "Chapter 73"),  (4, "Ep. 100"),      (5, "Chapter 56"),   (6, "Chapter 313"));
        Add(DayOfWeek.Friday,    (7, "Chapter 146"), (8, "Chapter 83"),   (9, "Chapter 289"),  (5, "Chapter 57"));
        Add(DayOfWeek.Saturday,  (1, "Chapter 152"), (2, "Chapter 112"),  (3, "Chapter 74"),   (5, "Chapter 58"));
        Add(DayOfWeek.Sunday,    (4, "Ep. 101"),     (6, "Chapter 314"),  (5, "Chapter 59"),   (9, "Chapter 290"));
    }

    private static Title C(int id, string name, string genre, string badge, string photo, string? tagline = null) =>
        new() { Id = id, Name = name, Author = "niji Original", Genre = genre, Kind = TitleKind.Comic, Badge = badge, Photo = photo, Tagline = tagline };

    private static Title N(int id, string name, string author, string genre, string badge, string photo, string? tagline = null) =>
        new() { Id = id, Name = name, Author = author, Genre = genre, Kind = TitleKind.Novel, Badge = badge, Photo = photo, Tagline = tagline };

    private SpotlightItem S(int titleId, string name, string desc, string tag1, string tag2) =>
        new(titleId, name, desc, tag1, tag2, _titles.First(t => t.Id == titleId).Image(600));

    private void Add(DayOfWeek day, params (int TitleId, string Episode)[] items)
    {
        foreach (var (titleId, episode) in items)
            _schedule.Add(new ScheduleEntry { Id = _schedule.Count + 1, Day = day, TitleId = titleId, Episode = episode });
    }

    public IReadOnlyList<Title> GetTitles(TitleKind kind) => _titles.Where(t => t.Kind == kind).ToList();

    public IReadOnlyList<Title> GetTrending(int take = 10) => GetTitles(TitleKind.Comic).Take(take).ToList();

    public Title? GetTitle(int id) => _titles.FirstOrDefault(t => t.Id == id);

    public IReadOnlyList<Title> GetByIds(params int[] ids) =>
        ids.Select(id => _titles.First(t => t.Id == id)).ToList();

    public IReadOnlyList<SpotlightItem> GetSpotlight(TitleKind kind) =>
        _spotlight.Where(s => _titles.First(t => t.Id == s.TitleId).Kind == kind).ToList();

    public IReadOnlyList<(ScheduleEntry Entry, Title Title)> GetSchedule(DayOfWeek day) =>
        _schedule.Where(s => s.Day == day)
                 .Select(s => (s, _titles.First(t => t.Id == s.TitleId)))
                 .ToList();
}