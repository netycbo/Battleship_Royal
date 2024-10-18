using AutoMapper;
using Battleship_Royal.Api.Controllers;
using Battleship_Royal.Api.Dtos.Game;
using Battleship_Royal.Api.Handlers.Services.Interfaces;
using Battleship_Royal.Api.Requests.Game;
using Battleship_Royal.Api.Responses.Games;
using MediatR;

namespace Battleship_Royal.Api.Handlers
{
    public class PrepareGameVsHumanHandler(IUserIdService userIdService, IMapper mapper, IGameCacheService gameCacheService) : IRequestHandler<PrepareGameVsHumanRequest, PrepareGameVsHumanResponse>
    {
        public async Task<PrepareGameVsHumanResponse> Handle(PrepareGameVsHumanRequest request, CancellationToken cancellationToken)
        {
            var gameId = Guid.NewGuid().ToString();
            TimeOnly startTime = TimeOnly.FromDateTime(DateTime.UtcNow);
            TimeOnly? endTime = null;

            if (request.Player1NickName == null)
            {
                throw new ArgumentException("player NickName is required");
            }
            var player1Id = userIdService.GetUserId(request.Player1NickName);
            if (player1Id == "-1")
            {
                throw new ArgumentException("player1 is not authenticated");
            }
            var player2Id = userIdService.GetUserId(request.Player2NickName);
            if (player2Id == "-1")
            {
                throw new ArgumentException("player2 is not authenticated");
            }
            if (request.IsSpeedGame)
            {
                TimeOnly timerValue = request.Timer;

                if (timerValue <= TimeOnly.FromDateTime(DateTime.Now))
                {
                    throw new ArgumentException("Invalid timer value. It must be a future time.");
                }

                endTime = startTime.AddMinutes(timerValue.Minute).AddHours(timerValue.Hour);
            }

            var prepareGame = mapper.Map<PrepareGameVsHumanDto>(request);
            prepareGame.GameId = gameId;
            gameCacheService.SetGameAsync(gameId, prepareGame);
            return new PrepareGameVsHumanResponse
            {
                Data = prepareGame
            };
        }
    }
}
