using Battleship_Royal.Api.Dtos;
using Battleship_Royal.Api.Dtos.Game;
using Battleship_Royal.Api.Handlers.Services.Interfaces;
using Battleship_Royal.Data.DbContext;

namespace Battleship_Royal.Api.Handlers.Services
{
    public class LoadGameFromTemporaryGames(BattleshipsDbContext context) : ILoadGameFromTemporaryGames
    {
        public async Task LoadFromTemporaryGames(PrepareGameDto newGameDto)
        {
            throw new NotImplementedException();
        }
    }
}
