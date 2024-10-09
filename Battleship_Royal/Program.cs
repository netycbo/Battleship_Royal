using Battleship_Royal.Components;
using Battleship_Royal.Data.Services.GameServices;
using Battleship_Royal.Data.Services.GameServices.Interfaces;
using Battleship_Royal.Data.Services.HttpClientFactory;
using Battleship_Royal.Data.Services.IdentityServices;
using Battleship_Royal.Data.Services.IdentityServices.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddHttpClient();
builder.Services.AddScoped<CustomHttpClientFactory>();
builder.Services.AddTransient<IGameServices, GameServices>();
builder.Services.AddTransient<IIdentityServices, IdentityServices>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();


app.Run();
