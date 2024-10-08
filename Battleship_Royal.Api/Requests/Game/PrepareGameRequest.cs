using Battleship_Royal.Api.Responses.Games;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Battleship_Royal.Api.Requests.Game
{
    public class PrepareGameRequest : IRequest<PrepareGameResponse>
    {
        [Required]
        public string Player1 { get; set; }
        public string Player2 { get; set; } = string.Empty;
        [Range(1, 3, ErrorMessage = "Difficulty level must be between 1 and 3.")]
        public int DifficultyLevel { get; set; } = 0;
        public bool IsSpeedGame { get; set; } = false;
        public int Timer { get; set; } = 10;
    }
}