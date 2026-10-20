using System.Security.Cryptography;
using System.Text;
using Niji_Midterms.Models;

namespace Niji_Midterms.Services;

// Demo-only in-memory store. Swap for EF Core + a database later.
public class AccountService
{
    private static readonly object Gate = new();
    private static int _nextUserId = 2;
    private static int _nextEntryId = 1;

    private static readonly List<AppUser> Users =
    [
        new() { Id = 1, FullName = "Demo Reader", Email = "demo@niji.com", PasswordHash = Hash("niji123") }
    ];

    private static readonly List<LibraryEntry> Entries = [];

    public AppUser? CurrentUser { get; private set; }
    public bool IsLoggedIn => CurrentUser is not null;
    public event Action? Changed;

    private static string Hash(string value) =>
        Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(value)));

    // Returns an error message, or null on success.
    public string? Register(string fullName, string email, string password)
    {
        if (string.IsNullOrWhiteSpace(fullName)) return "Full name is required.";

        lock (Gate)
        {
            if (Users.Any(u => u.Email.Equals(email.Trim(), StringComparison.OrdinalIgnoreCase)))
                return "An account with that email already exists.";

            var user = new AppUser
            {
                Id = _nextUserId++,
                FullName = fullName.Trim(),
                Email = email.Trim(),
                PasswordHash = Hash(password)
            };
            Users.Add(user);
            CurrentUser = user;
        }
        Changed?.Invoke();
        return null;
    }

    public bool Login(string email, string password)
    {
        AppUser? user;
        lock (Gate)
        {
            user = Users.FirstOrDefault(u =>
                u.Email.Equals(email.Trim(), StringComparison.OrdinalIgnoreCase) &&
                u.PasswordHash == Hash(password));
        }
        if (user is null) return false;

        CurrentUser = user;
        Changed?.Invoke();
        return true;
    }

    public void Logout()
    {
        CurrentUser = null;
        Changed?.Invoke();
    }

    public IReadOnlyList<LibraryEntry> GetLibrary()
    {
        if (CurrentUser is null) return [];
        lock (Gate) return Entries.Where(e => e.UserId == CurrentUser.Id).OrderByDescending(e => e.AddedOn).ToList();
    }

    public bool IsInLibrary(int titleId)
    {
        if (CurrentUser is null) return false;
        lock (Gate) return Entries.Any(e => e.UserId == CurrentUser.Id && e.TitleId == titleId);
    }

    // Returns true if the title is now in the library, false if it was removed.
    public bool ToggleLibrary(int titleId)
    {
        if (CurrentUser is null) return false;
        lock (Gate)
        {
            var existing = Entries.FirstOrDefault(e => e.UserId == CurrentUser.Id && e.TitleId == titleId);
            if (existing is not null)
            {
                Entries.Remove(existing);
                return false;
            }
            Entries.Add(new LibraryEntry { Id = _nextEntryId++, UserId = CurrentUser.Id, TitleId = titleId });
            return true;
        }
    }

    public void UpdateStatus(int entryId, ReadingStatus status)
    {
        lock (Gate) { var e = Entries.FirstOrDefault(x => x.Id == entryId); if (e is not null) e.Status = status; }
    }

    public void Remove(int entryId)
    {
        lock (Gate) Entries.RemoveAll(e => e.Id == entryId);
    }
}