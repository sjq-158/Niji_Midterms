using Niji_Midterms.Models;

namespace Niji_Midterms.Services;

// File names inside wwwroot/images/manhwas/
public static class Covers
{
    public const string SoloLeveling     = "solo-leveling.jpg";
    public const string OmniscientReader = "omniscient-reader.jpg";
    public const string TheBoxer         = "the-boxer.jpg";
    public const string UnderTheOakTree  = "under-the-oak-tree.jpg";
    public const string TrashOfTheCount  = "trash-of-the-count-family.jpg";
    public const string Lookism          = "lookism.jpg";
    public const string Eleceed          = "eleceed.jpg";
    public const string SssClassHunter   = "sss-class-hunter.jpg";
    public const string WindBreaker      = "wind-breaker.jpg";
    public const string SweetHome        = "sweet-home.jpg";
}

public class CatalogService
{
    // Demo data: which titles count as "Completed".
    private static readonly HashSet<int> CompletedIds = [1, 3, 10, 14, 15];

    private readonly List<Title> _titles;
    private readonly List<ScheduleEntry> _schedule = [];
    private readonly List<SpotlightItem> _spotlight;

    public CatalogService()
    {
        _titles =
        [
            // Comics (ids 1-10, same order as Top Trending on the home page)
            C(1, "Solo Leveling", "Action · Fantasy", "NEW", Covers.SoloLeveling, "Rise from the weakest hunter to the sovereign of shadows."),
            C(2, "Omniscient Reader", "Apocalypse · Fantasy", "UP", Covers.OmniscientReader, "The world has ended, and a new novel has begun."),
            C(3, "The Boxer", "Sports · Drama", "HIT", Covers.TheBoxer, "A journey beyond human limits in the ring."),
            C(4, "Trash of Count's Family", "Fantasy · Isekai", "HOT", Covers.TrashOfTheCount, "I just want to live a peaceful life here."),
            C(5, "Under the Oak Tree", "Romance · Fantasy", "NEW", Covers.UnderTheOakTree, "A timid noblewoman and a fierce knight in a grand romance."),
            C(6, "Lookism", "Drama · Action", "HIT", Covers.Lookism, "Two bodies, completely different daily lives."),
            C(7, "Eleceed", "Action · Supernatural", "UP", Covers.Eleceed, "Awakened powers and secret agent cats."),
            C(8, "SSS-Class Hunter", "Action · Fantasy", "NEW", Covers.SssClassHunter),
            C(9, "Wind Breaker", "Sports · Action", "HIT", Covers.WindBreaker),
            C(10, "Sweet Home", "Horror · Thriller", "TOP", Covers.SweetHome, "Desire monsters plague the apartment complex."),

            // Novels (ids 11-20). The number before the tagline is a demo volume count.
            N(11, "Under the Oak Tree", "Suji Kim", "Romance · Fantasy", "BESTSELLER", Covers.UnderTheOakTree, 7, "Stuttering lady meets a hero from the crusade."),
            N(12, "Omniscient Reader", "Sing Shong", "Apocalypse · Fantasy", "EVENT", Covers.OmniscientReader, 10, "The end of the world is only the beginning."),
            N(13, "Trash of Count's Family", "Yoo Ryeo Han", "Fantasy · Isekai", "POPULAR", Covers.TrashOfTheCount, 9, "I just want to live a quiet life as a slacker."),
            N(14, "Solo Leveling", "Chugong", "Action · Fantasy", "FREE PASS", Covers.SoloLeveling, 8, "Shadow monarch rises from dark dungeons."),
            N(15, "The Boxer", "JH", "Sports · Drama", "COMPLETE", Covers.TheBoxer, 6, "Gifted with overwhelming boxing prowess."),
            N(16, "Lookism", "Park Tae-jun", "Drama · Action", "NOVEL", Covers.Lookism, 5),
            N(17, "Eleceed", "Zhena / Son Jeho", "Action · Supernatural", "NOVEL", Covers.Eleceed, 4),
            N(18, "SSS-Class Hunter", "Shin Noah", "Action · Fantasy", "EXCLUSIVE", Covers.SssClassHunter, 3, "Dying to copy skills of the strongest."),
            N(19, "Wind Breaker", "Jo Yongseok", "Sports · Action", "NOVEL", Covers.WindBreaker, 2),
            N(20, "Sweet Home", "Kim Carnby", "Horror · Thriller", "NOVEL", Covers.SweetHome, 1),
        ];

        _spotlight =
        [
            // Comics spotlight (2 pages of 3)
            S(5, "Under the Oak Tree", "A timid noblewoman and a fierce knight in a grand romance.", "HIT", "ROMANCE"),
            S(3, "The Boxer", "A journey beyond human limits in the ring.", "EVENT", "SPORTS"),
            S(6, "Lookism", "Two bodies, completely different daily lives.", "EXCLUSIVE", "HIT"),
            S(7, "Eleceed", "Awakened powers and secret agent cats.", "ACTION", "SUPERPOWER"),
            S(10, "Sweet Home", "Desire monsters plague the apartment complex.", "THRILLER", "ORIGINAL"),
            S(1, "Solo Leveling", "The weakest hunter becomes sovereign.", "FANTASY", "TOP 10"),

            // Novels showcase (2 pages of 3)
            S(11, "Under the Oak Tree", "Stuttering lady meets a hero from the crusade.", "BESTSELLER", "ROMANCE"),
            S(12, "Omniscient Reader's Viewpoint", "The end of the world is only the beginning.", "EVENT", "NOVEL"),
            S(14, "Solo Leveling", "Shadow monarch rises from dark dungeons.", "ACTION", "FREE PASS"),
            S(13, "Trash of Count's Family", "I just want to live a quiet life as a slacker.", "POPULAR", "NOVEL"),
            S(15, "The Boxer", "Gifted with overwhelming boxing prowess.", "DRAMA", "COMPLETE"),
            S(18, "SSS-Class Revival Hunter", "Dying to copy skills of the strongest.", "EXCLUSIVE", "NOVEL"),
        ];

        // Weekly schedule
        Add(DayOfWeek.Monday,    (1, "Chapter 150"), (2, "Chapter 110"),  (3, "Chapter 72"),   (5, "Chapter 54"));
        Add(DayOfWeek.Tuesday,   (5, "Chapter 54"),  (6, "Chapter 312"),  (7, "Chapter 145"),  (8, "Chapter 82"));
        Add(DayOfWeek.Wednesday, (9, "Chapter 288"), (10, "Chapter 120"), (1, "Chapter 151"),  (5, "Chapter 55"));
        Add(DayOfWeek.Thursday,  (3, "Chapter 73"),  (4, "Ep. 100"),      (5, "Chapter 56"),   (6, "Chapter 313"));
        Add(DayOfWeek.Friday,    (7, "Chapter 146"), (8, "Chapter 83"),   (9, "Chapter 289"),  (5, "Chapter 57"));
        Add(DayOfWeek.Saturday,  (1, "Chapter 152"), (2, "Chapter 112"),  (3, "Chapter 74"),   (5, "Chapter 58"));
        Add(DayOfWeek.Sunday,    (4, "Ep. 101"),     (6, "Chapter 314"),  (5, "Chapter 59"),   (9, "Chapter 290"));
    }

    private static Title C(int id, string name, string genre, string badge, string cover, string? tagline = null) =>
        new()
        {
            Id = id, Name = name, Author = "niji Original", Genre = genre, Kind = TitleKind.Comic,
            Badge = badge, Cover = cover, Tagline = tagline, IsCompleted = CompletedIds.Contains(id)
        };

    private static Title N(int id, string name, string author, string genre, string badge, string cover, int volumes, string? tagline = null) =>
        new()
        {
            Id = id, Name = name, Author = author, Genre = genre, Kind = TitleKind.Novel,
            Badge = badge, Cover = cover, Tagline = tagline, Volumes = volumes, IsCompleted = CompletedIds.Contains(id)
        };

    private SpotlightItem S(int titleId, string name, string desc, string tag1, string tag2) =>
        new(titleId, name, desc, tag1, tag2, _titles.First(t => t.Id == titleId).ImageUrl);

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

    // Titles that release a chapter on the given day (used by "Daily Updates").
    public IReadOnlyList<Title> GetScheduledTitles(DayOfWeek day) =>
        GetSchedule(day).Select(s => s.Title).DistinctBy(t => t.Id).ToList();

    // Distinct genres for the dropdown menus.
    public IReadOnlyList<string> GetGenres(TitleKind kind) =>
        _titles.Where(t => t.Kind == kind)
               .SelectMany(t => t.Genres)
               .Distinct()
               .OrderBy(g => g)
               .ToList();

    // Matches name, author, or genre (case-insensitive).
    public IReadOnlyList<Title> Search(string? query)
    {
        if (string.IsNullOrWhiteSpace(query)) return [];
        var q = query.Trim();
        return _titles.Where(t =>
                t.Name.Contains(q, StringComparison.OrdinalIgnoreCase) ||
                t.Author.Contains(q, StringComparison.OrdinalIgnoreCase) ||
                t.Genre.Contains(q, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }
}