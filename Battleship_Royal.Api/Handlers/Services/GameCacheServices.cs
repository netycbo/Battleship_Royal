
using Battleship_Royal.Api.Dtos.Game;
using Battleship_Royal.Api.Handlers.Services.Interfaces;
using Battleship_Royal.Data.Entities;
using StackExchange.Redis;
using StackExchange.Redis.Extensions.Core.Abstractions;
using System.Text.Json;

namespace Battleship_Royal.Api.Handlers.Services
{
    public class GameCacheService : IGameCacheService
    {
        private readonly IDatabase _database;
        

        public GameCacheService(string connectionString)
        {
            var redis = ConnectionMultiplexer.Connect(connectionString);
            _database = redis.GetDatabase();
            
        }

        public async Task SetGameAsync(string key, object gameData, TimeSpan? expiry = null)
        {
            string jsonData = JsonSerializer.Serialize(gameData, new JsonSerializerOptions { WriteIndented = true });
            Console.WriteLine($"Setting Key: {key}, Data: {jsonData}");
            await _database.StringSetAsync(key, jsonData, expiry);
        }

        public async Task<string> GetGameAsync<T>(string key)
        {

            var jsonData = await _database.StringGetAsync(key);
            Console.WriteLine($"Setting Key: {key}, Data: {jsonData}");
            if (jsonData.IsNullOrEmpty) return default;

            return jsonData;
        }
        public async Task<bool> DeleteGameAsync(string key)
        {
            return await _database.KeyDeleteAsync(key);
        }
    }
}