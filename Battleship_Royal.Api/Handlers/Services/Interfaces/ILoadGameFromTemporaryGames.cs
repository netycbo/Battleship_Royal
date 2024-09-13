using Battleship_Royal.Api.Dtos;

namespace Battleship_Royal.Api.Handlers.Services.Interfaces
{
    public interface ILoadGameFromTemporaryGames
    {
        Task LoadFromTemporaryGames(NewGameDto newGameDto);
    }
}