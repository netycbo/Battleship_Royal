using Battleship_Royal.Api.Dtos;
using Battleship_Royal.Api.Handlers.Services.Interfaces;
using Battleship_Royal.Data.DbContext;

namespace Battleship_Royal.Api.Handlers.Services
{
    public class SaveGamesServices(BattleshipsDbContext context) : ISaveGamesServices
    {
        public async Task SaveToGamesAsync(NewGameDto newGameDto)
        {
            throw new NotImplementedException();
        }
        public async Task SaveToTemporaryGameAsync(NewGameDto temporaryGame)
        {
            throw new NotImplementedException();
        }
    }
}
