using Battleship_Royal.Api.Requests.Game;
using Battleship_Royal.Api.Responses.Games;

namespace Battleship_Royal.Data.Services.GameServices.Interfaces
{
    public interface IGameServices
    {
        Task<PrepareGameResponse>PrepareMatchAsync(PrepareGameRequest request);
        Task<RematchRersponse> GetGameSettingsFromRedisAsync(RematchRequest request);
        Task<SaveToDbResponse> SaveToDbAsync(SaveToDbRequest request);
        Task<GameStatsResponse> GameStatsAsync(GameStatsRequest request);
    }
}
