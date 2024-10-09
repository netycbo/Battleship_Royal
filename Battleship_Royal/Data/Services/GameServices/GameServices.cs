using Battleship_Royal.Api.Requests.Game;
using Battleship_Royal.Api.Responses.Games;
using Battleship_Royal.Data.Entities;
using Battleship_Royal.Data.Services.GameServices.Interfaces;
using Battleship_Royal.Data.Services.HttpClientFactory;
using System.Net.Http.Json;

namespace Battleship_Royal.Data.Services.GameServices
{
    public class GameServices(CustomHttpClientFactory clientFactory) : IGameServices
    {
        public async Task<GameStatsResponse> GameStatsAsync(GameStatsRequest request)
        {
            var client = clientFactory.CreateClient("player-stats");
            var response = await client.PostAsJsonAsync<GameStatsRequest>("player-stats", request);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<GameStatsResponse>();
        }

        public async Task<RematchRersponse> GetGameSettingsFromRedisAsync(RematchRequest request)
        {
            var client = clientFactory.CreateClient("rematch");
            var response = await client.PostAsJsonAsync<RematchRequest>("rematch", request);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<RematchRersponse>();
        }

        public async Task<PrepareGameResponse> PrepareMatchAsync(PrepareGameRequest request)
        {
            var client = clientFactory.CreateClient("prepare-match");
            var response = await client.PostAsJsonAsync<PrepareGameRequest>("prepare-match", request);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<PrepareGameResponse>();
        }

        public async Task<SaveToDbResponse> SaveToDbAsync(SaveToDbRequest request)
        {
            var client = clientFactory.CreateClient("save-To-Db");
            var response = await client.PostAsJsonAsync<SaveToDbRequest>("save-To-Db", request);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<SaveToDbResponse>();
        }
    }
}
