using Battleship_Royal.Api.Responses.Games;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Battleship_Royal.Api.Requests.Game
{
    public class SaveToDbRequest : IRequest<SaveToDbResponse>
    {

        [Required]
        public string GameId { get; set; }
        public DateTime DateOfGame { get; set; } = DateTime.Now;
        [Required]
        public string Player1Id { get; set; } = string.Empty;
        [Required]
        public string Player2Id { get; set; } = string.Empty;
        [Required]
        public string WinnerId { get; set; } = string.Empty;
        public TimeOnly BeginningOfGame { get; set; } = TimeOnly.FromDateTime(DateTime.Now);

        public TimeOnly EndingOfGame { get; set; } = TimeOnly.FromDateTime(DateTime.Now);
    }
}