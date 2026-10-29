# 🌸 niji - Webcomics & Webnovels Platform

**niji** is a Blazor web app for browsing and tracking webcomics, manhwas, and light novels. It has a light/dark theme, a weekly release schedule, working filters, search, and a personal reading library.

## Features

* **Home:** hero carousel, weekly lineup tabs, top trending ranking, genre tiles, and an exclusive webnovel banner.
* **Comics and Novels:** spotlight carousels plus working filters (genre dropdown, daily updates, completed, trending, by volume).
* **Search:** search titles, authors, and genres from the header.
* **Title details:** add or remove a title from your library.
* **Accounts and library:** register or sign in, then set reading status (Reading, Plan To Read, Completed). Demo account: `demo@niji.com` / `niji123`.
* **Theme toggle:** light and dark mode saved in the browser.

## Tech Stack

* ASP.NET Core Blazor (.NET 8, Interactive Server)
* Tailwind CSS v4 (CLI)
* In-memory demo data (no database)

## Run Locally

```bash
git clone https://github.com/sjq-158/Niji_Midterms.git
cd Niji_Midterms
npm install
npm run tailwind:build
dotnet run --launch-profile http
```

Open http://localhost:5008. While developing, keep `npm run tailwind:watch` running in a second terminal.

## Project Structure

```text
Niji_Midterms/
├── Components/
│   ├── Layout/MainLayout.razor      # header, search, footer
│   ├── Pages/                       # Home, Comics, Novels, Search, TitleDetails, Library, Auth
│   └── Shared/                      # CoverCard, SpotlightCarousel, GenreMenu
├── Models/                          # Title, AppUser, LibraryEntry, ScheduleEntry, SpotlightItem
├── Services/                        # CatalogService, AccountService
├── Styles/app.css                   # Tailwind source
└── wwwroot/
    ├── css/tailwind.css             # generated (git-ignored)
    └── images/manhwas/              # local cover images
```