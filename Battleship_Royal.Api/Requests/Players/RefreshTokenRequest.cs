using Battleship_Royal.Api.Responses.Players;
using MediatR;

namespace Battleship_Royal.Api.Requests.Players
{
    public class RefreshTokenRequest : IRequest<RefreshTokenResponse>
    {
        public string RefreshToken { get; set; }
    }
}
