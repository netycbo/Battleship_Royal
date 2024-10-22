using AutoMapper;
using Battleship_Royal.Api.Dtos.Game;
using Battleship_Royal.Api.Handlers.Services.Interfaces;
using Battleship_Royal.Api.Requests.Game;
using Battleship_Royal.Api.Responses.Games;
using Battleship_Royal.Data.DbContext;
using MediatR;

namespace Battleship_Royal.Api.Handlers
{
    public class SaveToDbHandler(IMapper mapper, ISaveGamesServices saveGames)
                                : IRequestHandler<SaveToDbRequest, SaveToDbResponse>
    {
        public async Task<SaveToDbResponse> Handle(SaveToDbRequest request, CancellationToken cancellationToken)
        {
            SaveToDbDto readyToSave = null;
            try
            {
                if (request.GameId.Any() && request.WinnerId.Any() && request.Player1NickName.Any() && request.Player2NickName.Any() &&
                    request.DateOfGame != default(DateTime) && request.BeginningOfGame != default(TimeOnly) && request.EndingOfGame != default(TimeOnly))
                {
                    if (request.Player2NickName == "1")
                    {
                        var readyToSaveWithComputerPlayer = mapper.Map<SaveToDbWithComputerPlayerDto>(request);
                        await saveGames.SaveToDbWithComputerPlayerAsync(readyToSaveWithComputerPlayer);
                    }
                    else
                    {
                        readyToSave = mapper.Map<SaveToDbDto>(request);
                        await saveGames.SaveToDbAsync(readyToSave);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Missing data in request: {ex.Message}");
            }
             
            return new SaveToDbResponse
            {
                Data = readyToSave
            };
        }

    }
}