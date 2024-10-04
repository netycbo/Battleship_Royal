using AutoMapper;
using Battleship_Royal.Api.Dtos.Game;
using Battleship_Royal.Api.Handlers.Services.Interfaces;
using Battleship_Royal.Api.Requests.Game;
using Battleship_Royal.Api.Responses.Games;
using Battleship_Royal.Data.DbContext;
using MediatR;

namespace Battleship_Royal.Api.Handlers
{
    public class SaveToDbHandler(BattleshipsDbContext context, IMapper mapper, ISaveGamesServices saveGames)
                                : IRequestHandler<SaveToDbRequest, SaveToDbResponse>
    {
        public async Task<SaveToDbResponse> Handle(SaveToDbRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.GameId.Any() && request.WinnerId.Any() && request.Player1Id.Any() && request.Player2Id.Any() &&
                    request.DateOfGame != default(DateTime) && request.BeginningOfGame != default(TimeOnly) && request.EndingOfGame != default(TimeOnly))
                {
                    var readyToSave = mapper.Map<SaveToDbDto>(request);
                    await saveGames.SaveToDbAsync(readyToSave);
                    return new SaveToDbResponse
                    {
                        Data = readyToSave
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Missing data in request");
            }
            return new SaveToDbResponse
            {
                Data = null
            };

            
        }
    }
}
