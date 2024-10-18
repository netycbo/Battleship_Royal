
namespace Battleship_Royal.Data.Services.HttpClientFactory
{
    public class CustomHttpClientFactory(IHttpClientFactory httpClientFactory) : ICustomHttpClientFactory
    {
        public HttpClient CreateClient(string endpoint)
        {
            var client = httpClientFactory.CreateClient();

            switch (endpoint.ToLower())
            {
                case "register":
                    client.BaseAddress = new Uri("https://localhost:7063/api/Identity/register");
                    break;

                case "login":
                    client.BaseAddress = new Uri("https://localhost:7063/api/Identity/login");
                    break;

                case "refresh-token":
                    client.BaseAddress = new Uri("https://yourapi.com/api/auth/");
                    break;
                case "player-stats":
                    client.BaseAddress = new Uri("https://localhost:7063/api/Game/player-stats");
                    break;
                case "rematch":
                    client.BaseAddress = new Uri("https://yourapi.com/api/");
                    break;
                case "prepare-gameVsComputer":
                    client.BaseAddress = new Uri("https://localhost:7063/api/Game/prepare-gameVsComputer");
                    break;
                case "prepare-gameVsHuman":
                    client.BaseAddress = new Uri("https://localhost:7063/api/Game/prepare-gameVsHuman");
                    break;
                case "save-To-Db":
                    client.BaseAddress = new Uri("https://localhost:7063/api/Game/");
                    break;


                default:
                    throw new ArgumentException("Invalid endpoint");
            }

            return client;
        }
    }
}
