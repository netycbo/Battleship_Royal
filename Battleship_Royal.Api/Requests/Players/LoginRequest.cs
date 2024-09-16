using Battleship_Royal.Api.Responses.Players;
using MediatR;

namespace Battleship_Royal.Api.Requests.Players
{
    public class LoginRequest : IRequest<LoginResponse>
    {
        public string NickName { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
