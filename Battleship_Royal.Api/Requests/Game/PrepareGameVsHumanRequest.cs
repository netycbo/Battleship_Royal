using Battleship_Royal.Api.Responses.Games;
using MediatR;

namespace Battleship_Royal.Api.Controllers
{
    public class PrepareGameVsHumanRequest : IRequest<PrepareGameVsHumanResponse>
    {
        public string Player1NickName { get; set; }
        public string Player2NickName { get; set; }
        public bool IsSpeedGame { get; set; } = false;
        public TimeOnly Timer { get; set; } = new TimeOnly(10, 10, 0, 0);

    }
}