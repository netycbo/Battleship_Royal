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
    public class PrepareGameHandler(IMapper mapper, IUserIdService idService,
            UserManager<ApplicationUser> userManager, BattleshipsDbContext context, IGameCacheService gameCacheService)
        : IRequestHandler<PrepareGameVsComputerRequest, PrepareGameVsComputerResponse>
    {
        public async Task<PrepareGameVsComputerResponse> Handle(PrepareGameVsComputerRequest request, CancellationToken cancellationToken)
        {
            var gameId = Guid.NewGuid().ToString();
            TimeOnly startTime = TimeOnly.FromDateTime(DateTime.UtcNow);
            TimeOnly? endTime = null;

            if (request.PlayerNickName != null)
            {
                var player1Id = idService.GetUserId(request.PlayerNickName);
                if (player1Id == "-1")
                {
                    throw new ArgumentException("player1 is not authenticated");
                }

                var player1 = await userManager.FindByIdAsync(player1Id);
                if (player1 == null)
                {
                    throw new Exception($"Player1 with ID {player1Id} not found.");
                }

                var difficulty = request.DifficultyLevel;

                if (request.IsSpeedGame)
                {
                    int timerMinutes = request.Timer;

                    if (timerMinutes <= 0)
                    {
                        throw new ArgumentException("Invalid timer value. It must be greater than 0.");
                    }

                    endTime = startTime.AddMinutes(timerMinutes);
                }

                var prepareGameReady = mapper.Map<PrepareGameVsComputerDto>(request);
                prepareGameReady.GameId = gameId;
                prepareGameReady.DifficultyLevel = difficulty;
                gameCacheService.SetGameAsync(gameId, prepareGameReady);

                return new PrepareGameVsComputerResponse
                {
                    Data = prepareGameReady,
                    
                };
            }

            // Handle the case when PlayerName is null (if needed)
            throw new ArgumentException("Player name cannot be null.");
        }
    }
}