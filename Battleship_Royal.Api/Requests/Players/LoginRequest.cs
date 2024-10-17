using Battleship_Royal.Api.Responses.Players;
using MediatR;

namespace Battleship_Royal.Api.Requests.Players
{
    public class LoginRequest : IRequest<LoginResponse>
    {
        public string NickName { get; set; }
        public string Email { get; set; }
        //public TimeOnly StartSession { get; set; }  = TimeOnly.FromDateTime(DateTime.Now);
        public string Password { get; set; }
    }
}
