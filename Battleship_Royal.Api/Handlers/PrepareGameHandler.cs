using AutoMapper;
using Battleship_Royal.Api.Dtos.Game;
using Battleship_Royal.Api.Handlers.Services.Interfaces;
using Battleship_Royal.Api.Requests.Game;
using Battleship_Royal.Api.Responses.Games;
using Battleship_Royal.Data.DbContext;
using Battleship_Royal.Data.Entities;
using Battleship_Royal.Data.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Battleship_Royal.Api.Handlers
{
    public class PrepareGameHandler(IMapper mapper, ISaveGamesServices saveGamesServices, IUserIdService idService,
            UserManager<ApplicationUser> userManager, BattleshipsDbContext context)
        : IRequestHandler<PrepareGameRequest, PrepareGameResponse>
    {
        public async Task<PrepareGameResponse> Handle(PrepareGameRequest request, CancellationToken cancellationToken)
        {
            var gameId = Guid.NewGuid().ToString();
            TimeOnly startTime = TimeOnly.FromDateTime(DateTime.UtcNow);
            TimeOnly? endTime = null;

            if (request.Player1 != null) 
            {
                var player1Id = idService.GetUserId();
                if (player1Id == "-1")
                {
                    throw new ArgumentException("player1 is not autenticated");
                }
                var player1 = await userManager.FindByIdAsync(player1Id);
                if (player1.IsInGame)
                {
                    throw new Exception($"player1: {request.Player1} is allredy in game");
                }
            }
            if (request.Player2 != null)
            {
                var player2Id = idService.GetUserId();
                if (player2Id == "-1")
                {
                    throw new ArgumentException("player1 is not autenticated");
                }
                var player2 = await userManager.FindByIdAsync(player2Id);
                if (player2.IsInGame)
                {
                    throw new Exception($"player1: {request.Player2} is allredy in game");
                }
            }
            else
            {
                var computerPlayer = context.ComputerPlayers.Find("Red October");
                request.Player2 = computerPlayer.NickName;
            }
            if(request.IsSpeedGame)
            {
                int timerMinutes = request.Timer;

                if (timerMinutes <= 0)
                {
                    throw new ArgumentException("Invalid timer value. It must be greater than 0.");
                }

                endTime = startTime.AddMinutes(timerMinutes);
            }

            var prepareGameReady = mapper.Map<PrepareGameDto>(request);
            var saveToTemporaryDb = saveGamesServices.SaveToTemporaryGameAsync(prepareGameReady);
            return new PrepareGameResponse
            {
                Data = prepareGameReady
            };
        }
           
        
    }
}
