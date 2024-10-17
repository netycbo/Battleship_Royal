using Battleship_Royal.Api.Handlers.Services.Interfaces;
using Battleship_Royal.Api.Handlers.Services;
using Battleship_Royal.Api.Mappings;
using Battleship_Royal.Components;
using Battleship_Royal.Data.Services.GameServices.Interfaces;
using Battleship_Royal.Data.Services.GameServices;
using Battleship_Royal.Data.Services.HttpClientFactory;
using Battleship_Royal.Data.Services.IdentityServices.Interfaces;
using Battleship_Royal.Data.Services.IdentityServices;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpClient("", client =>
{
    client.BaseAddress = new Uri("https://localhost:7063/");
});
builder.Services.AddTransient<ICustomHttpClientFactory, CustomHttpClientFactory>();
builder.Services.AddTransient<IGameServices, GameServices>();
builder.Services.AddTransient<IIdentityServices, IdentityServices>();
builder.Services.AddTransient<IDeserializeService, DeserializeService>();
builder.Services.AddAutoMapper(typeof(Profiles).GetTypeInfo().Assembly, typeof(Profiles).Assembly);
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
