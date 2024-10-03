using AutoMapper;
using Battleship_Royal.Api.Dtos.Game;
using Battleship_Royal.Api.Handlers.Services.Interfaces;
using Battleship_Royal.Api.Requests.Game;
using Battleship_Royal.Api.Responses.Games;
using Battleship_Royal.Data.DbContext;
using Battleship_Royal.Data.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Battleship_Royal.Api.Handlers
{
    public class PrepareGameHandler(IMapper mapper, ISaveGamesServices saveGamesServices, IUserIdService idService,
            UserManager<ApplicationUser> userManager, BattleshipsDbContext context, IGameCacheService gameCacheService)
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
                    throw new ArgumentException("player1 is not authenticated");
                }

                var player1 = await userManager.FindByIdAsync(player1Id);
                if (player1 == null)
                {
                    throw new Exception($"Player1 with ID {player1Id} not found.");
                }

                // Debugging: ensure player1 object is properly populated
                if (player1.IsInGame == null)
                {
                    throw new Exception($"IsInGame property is null for player1: {player1.UserName}.");
                }

                if (player1.IsInGame)
                {
                    throw new Exception($"Player1: {player1.UserName} is already in game.");
                }
            }
            if (request.Player2 == null)
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
                var computerPlayer = context.ComputerPlayers.FirstOrDefault();

                if (computerPlayer == null)
                {
                    throw new Exception("No computer player found in the system.");
                }

                Console.WriteLine($"Computer player found: {computerPlayer.NickName}");

                // Przypisz NickName komputerowego gracza do Player2 w żądaniu
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
            var saveToRedis = gameCacheService.SetGameAsync(gameId,prepareGameReady);

            return new PrepareGameResponse
            {
                Data = prepareGameReady,
                GameId = gameId
            };
        }
           
        
    }
}
