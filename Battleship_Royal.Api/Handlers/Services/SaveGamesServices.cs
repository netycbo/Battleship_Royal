using AutoMapper;
using Battleship_Royal.Api.Dtos;
using Battleship_Royal.Api.Dtos.Game;
using Battleship_Royal.Api.Handlers.Services.Interfaces;
using Battleship_Royal.Data.DbContext;
using Battleship_Royal.Data.Entities;

namespace Battleship_Royal.Api.Handlers.Services
{
    public class SaveGamesServices(BattleshipsDbContext context, IMapper mapper) : ISaveGamesServices
    {
        public async Task SaveToDbAsync(SaveToDbDto redyToSave)
        {
            
            var game = mapper.Map<Game>(redyToSave);
            context.Games.Add(game);
            await context.SaveChangesAsync();

        }

        public async Task SaveToDbWithComputerPlayerAsync(SaveToDbWithComputerPlayerDto SaveToDbWithComputerPlayerDto)
        {
            var game = mapper.Map<Game>(SaveToDbWithComputerPlayerDto);
            context.Games.Add(game);
            await context.SaveChangesAsync();
        }
    }
}
