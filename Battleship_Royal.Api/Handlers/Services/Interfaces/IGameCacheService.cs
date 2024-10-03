namespace Battleship_Royal.Api.Handlers.Services.Interfaces
{
    public interface IGameCacheService
    {
        Task<bool> DeleteGameAsync(string key);
        Task<string> GetGameAsync<T>(string key);
        Task SetGameAsync(string key, object gameData, TimeSpan? expiry = null);
    }
}