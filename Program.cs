using Niji_Midterms.Components;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<Niji_Midterms.Services.CatalogService>();
builder.Services.AddScoped<Niji_Midterms.Services.AccountService>();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();