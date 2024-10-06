using Battleship_Royal.Api.Responses.Games;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Battleship_Royal.Api.Requests.Game
{
    public class GameStatsRequest : IRequest<GameStatsResponse>
    {
        [Required]
        public string PlayerId { get; set; }
    }
}