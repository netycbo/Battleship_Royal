using Battleship_Royal.Api.Requests.Players;
using Battleship_Royal.Api.Responses.Players;

namespace Battleship_Royal.Data.Services.IdentityServices.Interfaces
{
    public interface IIdentityServices
    {
        Task<NewUserRegistrationResponse> RegisterAsync(NewUserRegistrationRequest request);
        Task<LoginResponse> LoginAsync(LoginRequest request);
        Task<RefreshTokenResponse> RefreshToken(RefreshTokenRequest request);
    }
}
