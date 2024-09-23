using Battleship_Royal.Api.Dtos;
using Battleship_Royal.Api.Dtos.Game;

namespace Battleship_Royal.Api.Handlers.Services.Interfaces
{
    public interface ISaveGamesServices
    {
        //Task SaveToGamesAsync(GameDto newGameDto);
        Task SaveToTemporaryGameAsync(PrepareGameDto newGame);
    }
}