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
        public async Task SaveToGamesAsync(GameDto newGameDto)
        {
            ArgumentNullException.ThrowIfNull(newGameDto);
            var game = mapper.Map<Game>(newGameDto);
            context.Games.Add(game);
            await context.SaveChangesAsync();

        }
        public async Task SaveToTemporaryGameAsync(PrepareGameDto temporaryGame)
        {
            try
            {
                if (temporaryGame == null)
                {
                    throw new ArgumentNullException(nameof(temporaryGame));
                }
                var game = mapper.Map<TemporaryGame>(temporaryGame);
                if (game == null)
                {
                    throw new Exception("Mapping failed: game is null");
                }
                context.TemporaryGames.AddRange(game);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while saving to database: {ex.Message}", ex);
            }
           
        }
    }
}
