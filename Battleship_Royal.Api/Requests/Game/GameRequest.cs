using Battleship_Royal.Api.Responses.Games;
using MediatR;

namespace Battleship_Royal.Api.Requests
{
    public class GameRequest : IRequest<GameResponse>
    {
    }
}