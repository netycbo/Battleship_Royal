using Battleship_Royal.Api.Requests.Players;
using Battleship_Royal.Api.Responses.Players;
using Battleship_Royal.Data.Services.HttpClientFactory;
using Battleship_Royal.Data.Services.IdentityServices.Interfaces;

namespace Battleship_Royal.Data.Services.IdentityServices
{
    public class IdentityServices(CustomHttpClientFactory clientFactory) : IIdentityServices
    {
        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var client = clientFactory.CreateClient("login");
            var response = await client.PostAsJsonAsync("login", request);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<LoginResponse>();
        }

        public async Task<RefreshTokenResponse> RefreshToken(RefreshTokenRequest request)
        {
            var client = clientFactory.CreateClient("refresh-token");
            var response = await client.PostAsJsonAsync("refresh-token", request);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<RefreshTokenResponse>();
        }

        public async Task<NewUserRegistrationResponse> RegisterAsync(NewUserRegistrationRequest request)
        {
            var client = clientFactory.CreateClient("register");
            var response = await client.PostAsJsonAsync("register", request);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<NewUserRegistrationResponse>();
        }
    }
}
