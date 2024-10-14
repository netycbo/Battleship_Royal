using Battleship_Royal.Api.Mappings;
using Battleship_Royal.Data.DbContext;
using Battleship_Royal.Data.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Battleship_Royal.Api.Handlers.Services.Interfaces;
using Battleship_Royal.Api.Handlers.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using StackExchange.Redis.Extensions.Core.Configuration;
using StackExchange.Redis;
using StackExchange.Redis.Extensions.Core.Abstractions;
using StackExchange.Redis.Extensions.Newtonsoft;
using Microsoft.Extensions.Options;
using Battleship_Royal.GameLogic;
using Battleship_Royal.GameLogic.ComputerPlayer.Interfaces;

using Battleship_Royal.GameLogic.ComputerPlayer.DifficultyLevels;
using Battleship_Royal.GameLogic.ComputerPlayer.DifficultyLevels.DifficultyServices.Interfaces;
using Battleship_Royal.GameLogic.ComputerPlayer.DifficultyLevels.DifficultyServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;


var builder = WebApplication.CreateBuilder(args);

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


builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddAutoMapper(typeof(Profiles).GetTypeInfo().Assembly, typeof(Profiles).Assembly);

builder.Services.AddTransient<ISaveGamesServices, SaveGamesServices>();
builder.Services.AddTransient<IUserIdService, UserIdService>();
builder.Services.AddTransient<IDeserializeService, DeserializeService>();
builder.Services.AddTransient<IGameBoard, GameBoard>();
builder.Services.AddTransient<IDifficultyStrategy, EasyLevel>();
builder.Services.AddTransient<IGenerateRandomCoordinates, GenerateRandomCoordinates>();
builder.Services.AddTransient<IBfsAlgorithm, BfsAlgorithm>();
builder.Services.AddScoped<IGenerateJwtToken , GenerateJwtToken>();
builder.Services.AddScoped<IGenerateRefreshToken, GenerateRefreshToken>();
builder.Services.AddScoped<UserManager<ApplicationUser>>();
builder.Services.AddSingleton<Random>();

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

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
