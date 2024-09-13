using Battleship_Royal.Api.Dtos;

namespace Battleship_Royal.Api.Handlers.Services.Interfaces
{
    public interface ISaveGamesServices
    {
        Task SaveToGamesAsync(NewGameDto newGameDto);
        Task SaveToTemporaryGameAsync(NewGameDto newGame);
    }
}