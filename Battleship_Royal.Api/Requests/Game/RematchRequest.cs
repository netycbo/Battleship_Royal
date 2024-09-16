using Battleship_Royal.Api.Responses.Games;
using MediatR;

namespace Battleship_Royal.Api.Requests.Game
{
    public class RematchRequest : IRequest<RematchRersponse>
    {
        public int GameId { get; set; }
    }
}