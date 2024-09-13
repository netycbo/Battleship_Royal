using Battleship_Royal.Api.Dtos;
using Battleship_Royal.Api.Handlers.Services.Interfaces;
using Battleship_Royal.Data.DbContext;

namespace Battleship_Royal.Api.Handlers.Services
{
    public class LoadGameFromTemporaryGames(BattleshipsDbContext context) : ILoadGameFromTemporaryGames
    {
        public async Task LoadFromTemporaryGames(NewGameDto newGameDto)
        {
            throw new NotImplementedException();
        }
    }
}
