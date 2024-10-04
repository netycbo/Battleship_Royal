using System.ComponentModel.DataAnnotations;

namespace Battleship_Royal.Api.Dtos.Game
{
    public class SaveToDbDto
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