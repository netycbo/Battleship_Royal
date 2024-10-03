using Battleship_Royal.Api.Dtos.Game;

namespace Battleship_Royal.Api.Responses.Games
{
    public class PrepareGameResponse : BaseResponse<PrepareGameDto>
    {
        public string GameId { get; set; }
    }
}