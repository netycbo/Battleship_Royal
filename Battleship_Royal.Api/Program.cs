﻿using Battleship_Royal.Api.Mappings;
using Battleship_Royal.Data.DbContext;
using Battleship_Royal.Data.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Battleship_Royal.Api.Handlers.Services.Interfaces;
using Battleship_Royal.Api.Handlers.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Battleship_Royal.GameLogic.ComputerPlayer.Interfaces;
using Battleship_Royal.GameLogic.ComputerPlayer.DifficultyLevels;
using Battleship_Royal.GameLogic.ComputerPlayer.DifficultyLevels.DifficultyServices.Interfaces;
using Battleship_Royal.GameLogic.ComputerPlayer.DifficultyLevels.DifficultyServices;
using Battleship_Royal.GameLogic.GameBoard.GameBoardServices;
using Battleship_Royal.GameLogic;
using Battleship_Royal.Data.Services.GameServices.Helpers;
using Battleship_Royal.GameLogic.GameBoard.GameBoardServices.Helpers.Interfaces;
using Battleship_Royal.GameLogic.GameBoard.GameBoardServices.Helpers;
using Battleship_Royal.GameLogic.GameContext.Interfaces;
using Battleship_Royal.GameLogic.GameContext;
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

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<BattleshipsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<BattleshipsDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddAutoMapper(typeof(Profiles).GetTypeInfo().Assembly, typeof(Profiles).Assembly);

builder.Services.AddTransient<ISaveGamesServices, SaveGamesServices>();
builder.Services.AddTransient<IUserIdService, UserIdService>();
builder.Services.AddTransient<IDeserializeService, DeserializeService>();

builder.Services.AddSingleton<IHasDifferentShape, HasDifferentShape>();
builder.Services.AddScoped<IGameBoardServices, GameBoardServices>();
builder.Services.AddScoped<IComputerShipPlacer, ComputerShipPlacer>();
builder.Services.AddTransient<IDifficultyStrategy, EasyLevel>();
builder.Services.AddTransient<IGenerateRandomCoordinates, GenerateRandomCoordinates>();
builder.Services.AddSingleton<IBoardInitializer, BoardInitializer>();
builder.Services.AddScoped<ICheckShipPlacement, CheckShipPlacement>();
builder.Services.AddScoped<IShipPlacer, ShipPlacer>();
builder.Services.AddScoped<IShipValidator, ShipValidator>();
builder.Services.AddTransient<IBfsAlgorithm, BfsAlgorithm>();
builder.Services.AddScoped<IBoardInitializer, BoardInitializer>();
builder.Services.AddScoped<IGameBoardServices, GameBoardServices>();
builder.Services.AddScoped<IComputerShipPlacer, ComputerShipPlacer>();
builder.Services.AddScoped<IGenerateJwtToken , GenerateJwtToken>();
builder.Services.AddScoped<IGenerateRefreshToken, GenerateRefreshToken>();
builder.Services.AddScoped<IGameContext, GameContext>();
builder.Services.AddScoped<UserManager<ApplicationUser>>();
builder.Services.AddSingleton<Random>();
builder.Services.AddSignalR();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });
builder.Services.Configure<RedisOptions>(builder.Configuration.GetSection("Redis"));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddSingleton<IGameCacheService, GameCacheService>(sp =>
{
    var options = sp.GetRequiredService<IOptions<RedisOptions>>().Value;
    string connectionString = $"{options.Host}:{options.Port},connectTimeout={options.ConnectTimeout}";

    return new GameCacheService(connectionString); 
});
builder.Services.AddControllers()
 .AddNewtonsoftJson();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAllOrigins");


//app.MapHub<GameHub>("/gamehub");

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
