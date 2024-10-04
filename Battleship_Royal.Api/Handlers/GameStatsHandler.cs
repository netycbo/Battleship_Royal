using AutoMapper;
using Battleship_Royal.Api.Dtos.Game;
using Battleship_Royal.Api.Requests.Game;
using Battleship_Royal.Api.Responses.Games;
using Battleship_Royal.Data.DbContext;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Battleship_Royal.Api.Handlers
{
    public class GameStatsHandler(BattleshipsDbContext context, IMapper mapper) : IRequestHandler<GameStatsRequest, GameStatsResponse>
    {
        public async Task<GameStatsResponse> Handle(GameStatsRequest request, CancellationToken cancellationToken)
        {
             
            if (request.PlayerId == null)
            {
                throw new Exception("PlayerId is dead");
            }

            var timeTotal = await context.Games
                .Where(g => g.Player1Id == request.PlayerId || g.Player2Id == request.PlayerId)
                .Select(g => g.EndingOfGame - g.BeginningOfGame)
                .ToListAsync(cancellationToken);
            TimeSpan totalTime = timeTotal.Aggregate(TimeSpan.Zero, (sum, time) => sum + time);

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
                    Name = group.Key,
                    GameCount = group.Count() 
                })
                .OrderByDescending(x => x.GameCount)
                .FirstOrDefaultAsync(); 

            var playerstats = new GameStatsDto
            {
                PlayerName = context.Users.Where(u => u.Id == request.PlayerId)
                .Select(u => u.NickName).FirstOrDefault(),
                NumbersOfGames = totalGames,
                NumbersOfWins = wins,
                NumbersOfLosses = losses,
                HoursOfPlay = totalTime,
                MostGamesWithPlayer = mostFrequentOpponent.Name
            };

            return new GameStatsResponse
            {
                Data = playerstats
            };
        }
    }
}
