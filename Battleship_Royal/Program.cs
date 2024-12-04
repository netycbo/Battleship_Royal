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
using Battleship_Royal.GameLogic.GameBoard.GameBoardServices;
using Battleship_Royal.GameLogic.ComputerPlayer.Interfaces;
using Battleship_Royal.GameLogic.GameBoard.GameBoardServices.Helpers.Interfaces;
using Battleship_Royal.GameLogic.GameBoard.GameBoardServices.Helpers;
using Battleship_Royal.GameLogic;
using Battleship_Royal.Data.Services.GameServices.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Battleship_Royal.GameLogic.GameContext;
using Battleship_Royal.GameLogic.GameContext.Interfaces;
using Battleship_Royal.GameLogic.ComputerPlayer;
using Battleship_Royal.Components.Game_Components;
using Serilog;
using Microsoft.AspNetCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/login-middleware-.txt", rollingInterval: RollingInterval.Day)
    .Enrich.FromLogContext()
    .CreateLogger();


builder.Host.UseSerilog();
// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor()
    .AddCircuitOptions(options =>
    {
        options.DetailedErrors = true;
    });
builder.Services.AddHttpClient("", client =>
{
    client.BaseAddress = new Uri("https://localhost:7063/");
});
builder.Services.AddScoped<IShipPlacer, ShipPlacer>();
builder.Services.AddScoped<ICustomHttpClientFactory, CustomHttpClientFactory>();
builder.Services.AddScoped<IGameServices, GameServices>();
builder.Services.AddTransient<IIdentityServices, IdentityServices>();
builder.Services.AddTransient<IDeserializeService, DeserializeService>();

builder.Services.AddScoped<IShipValidator, ShipValidator>();

builder.Services.AddSingleton<IStateContainer,StateContainer>();

builder.Services.AddSingleton<IHasDifferentShape, HasDifferentShape>();
builder.Services.AddScoped<IComputerShipPlacer, ComputerShipPlacer>();
builder.Services.AddSingleton<IBoardInitializer, BoardInitializer>();
builder.Services.AddScoped<IGameBoardServices, GameBoardServices>();
builder.Services.AddScoped<ISetGameBoard, SetGameBoard>();
builder.Services.AddScoped<ICheckShipPlacement, CheckShipPlacement>();

builder.Services.AddAutoMapper(typeof(Profiles).GetTypeInfo().Assembly, typeof(Profiles).Assembly);

builder.Services.AddTransient<ITurnLogic, TurnLogic>();

builder.Services.AddScoped<IGameContext, GameContext>();
builder.Services.AddServerSideBlazor().AddCircuitOptions(options =>
{
    options.DetailedErrors = true;
    options.JSInteropDefaultCallTimeout = TimeSpan.FromSeconds(60);
});
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
