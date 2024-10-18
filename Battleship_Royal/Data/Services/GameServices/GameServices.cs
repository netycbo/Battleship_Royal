using Battleship_Royal.Api.Controllers;
using Battleship_Royal.Api.Requests.Game;
using Battleship_Royal.Api.Responses.Games;
using Battleship_Royal.Data.Services.GameServices.Interfaces;
using Battleship_Royal.Data.Services.HttpClientFactory;

namespace Battleship_Royal.Data.Services.GameServices
{
    public class GameServices(ICustomHttpClientFactory clientFactory) : IGameServices
    {
        public async Task<GameStatsResponse> GameStatsAsync(GameStatsRequest request)
        {
            var client = clientFactory.CreateClient("player-stats");
            var query = $"?PlayerId={Uri.EscapeDataString(request.PlayerId)}";
            var response = await client.GetAsync(query);
            response.EnsureSuccessStatusCode();

            // Deserialize the JSON response into GameStatsResponse
            var gameStatsResponse = await response.Content.ReadFromJsonAsync<GameStatsResponse>();

            if (gameStatsResponse == null)
            {
                throw new InvalidOperationException("Failed to deserialize the response.");
            }

            return gameStatsResponse;
        }

        public async Task<RematchRersponse> GetGameSettingsFromRedisAsync(RematchRequest request)
        {
            var client = clientFactory.CreateClient("rematch");
            var query = $"?GameId={Uri.EscapeDataString(request.GameId)}";
            var response = await client.GetAsync(query);
            response.EnsureSuccessStatusCode();

            // Deserialize the JSON response into GameStatsResponse
            var remachResponse = await response.Content.ReadFromJsonAsync<RematchRersponse>();

            if (remachResponse == null)
            {
                throw new InvalidOperationException("Failed to deserialize the response.");
            }

            return remachResponse;
        }

        public async Task<PrepareGameVsComputerResponse> PrepareMatchVsComputerAsync(PrepareGameVsComputerRequest request)
        {
            var client = clientFactory.CreateClient("prepare-gameVsComputer");
            var response = await client.PostAsJsonAsync<PrepareGameVsComputerRequest>("", request);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<PrepareGameVsComputerResponse>();
        }
        public async Task<PrepareGameVsHumanResponse> PrepareMatchVsHumanAsync(PrepareGameVsHumanRequest request)
        {
            var client = clientFactory.CreateClient("prepare-gameVsHuman");
            var response = await client.PostAsJsonAsync<PrepareGameVsHumanRequest>("", request);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<PrepareGameVsHumanResponse>();
        }

        public async Task<SaveToDbResponse> SaveToDbAsync(SaveToDbRequest request)
        {
            var client = clientFactory.CreateClient("save-To-Db");
            var response = await client.PostAsJsonAsync<SaveToDbRequest>("", request);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<SaveToDbResponse>();
        }
    }
}
