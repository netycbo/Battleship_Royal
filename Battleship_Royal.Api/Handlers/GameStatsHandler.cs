using Battleship_Royal.Api.Dtos.Game;
using Battleship_Royal.Api.Handlers.Services.Interfaces;
using Battleship_Royal.Api.Requests.Game;
using Battleship_Royal.Api.Responses.Games;
using Battleship_Royal.Data.DbContext;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Battleship_Royal.Api.Handlers
{
    public class GameStatsHandler(BattleshipsDbContext context, IUserIdService idService ) : IRequestHandler<GameStatsRequest, GameStatsResponse>
    {
        public async Task<GameStatsResponse> Handle(GameStatsRequest request, CancellationToken cancellationToken)
        {
             
            if (request.PlayerId.IsNullOrEmpty())
            {
                throw new Exception("Player is dead");
            }

            var timeTotal = await context.Games
                .Where(g => g.Player1Id == request.PlayerId || g.Player2Id == request.PlayerId)
                .Select(g => g.EndingOfGame - g.BeginningOfGame)
                .ToListAsync(cancellationToken);

            TimeSpan totalTime = TimeSpan.Zero;
            if (timeTotal != null)
            {
                totalTime = timeTotal.Aggregate(TimeSpan.Zero, (sum, time) => sum + time);
            }
            else
            {
                Console.WriteLine("Start to play");
            }
            var wins = await context.Games
                .CountAsync(g => (g.Player1Id == request.PlayerId && g.WinnerNickName == g.Player1Id) ||
                             (g.Player2Id == request.PlayerId && g.WinnerNickName == g.Player2Id));

            var totalGames = await context.Games
                .SumAsync(g => (g.Player1Id == request.PlayerId || g.Player2Id == request.PlayerId) ? 1 : 0);

             var losses = totalGames - wins;

            var mostFrequentOpponent = await context.Games
                .Where(g => g.Player1Id == request.PlayerId || g.Player2Id == request.PlayerId)
                .GroupBy(g => g.Player1Id == request.PlayerId ? g.Player2Id : g.Player1Id) 
                .Select(group => new CounterPlayer
                {
                    Name = group.First().Player1Id == request.PlayerId ? group.First().Player2Id : group.First().Player1Id,
                    GameCount = group.Count() 
                })
                .OrderByDescending(x => x.GameCount)
                .FirstOrDefaultAsync();

            var games = await context.Games
                 .Where(g => (g.Player1Id == request.PlayerId || g.Player2Id == request.PlayerId) && g.WinnerNickName == request.PlayerId)
                 .ToListAsync(cancellationToken);

            var fastestGame = games
                .OrderBy(g => g.EndingOfGame - g.BeginningOfGame)
                .FirstOrDefault();

            int quickestWinInMinutes = 0;

            if (fastestGame != null)
            {
                TimeSpan quickestWinTime = fastestGame.EndingOfGame - fastestGame.BeginningOfGame;
                quickestWinInMinutes = (int)quickestWinTime.TotalMinutes;
            }
            else
            {
                Console.WriteLine("Winn your first game!");
            }
            try
            {
                var playerstats = new GameStatsDto
                {
                    PlayerName = idService.GetUserNickName(request.PlayerId),
                    NumbersOfGames = totalGames,
                    NumbersOfWins = wins,
                    NumbersOfLosses = losses,
                    HoursOfPlay = totalTime,
                    QuickestWinInMinutes = quickestWinInMinutes,
                    MostGamesWithPlayer = idService.GetUserNickName(mostFrequentOpponent.Name),
                    NumbersOfGamesWithThatPlayer = mostFrequentOpponent.GameCount
                };
                return new GameStatsResponse
                {
                    Data = playerstats
                };
            }
            catch
            {
                throw new Exception();
            }
                
            
        }
    }
}

