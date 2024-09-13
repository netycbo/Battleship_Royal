using System.ComponentModel.DataAnnotations;

namespace Battleship_Royal.Data.Entities
{
    public class Game
    {
        [Required]
        public int GameId { get; set; }
        public DateTime DateOfGame { get; set; } = DateTime.Now;
        [Required]
        public string Player1NickName { get; set; } = string.Empty;
        [Required]
        public string Player2NickName { get; set; } = string.Empty;
        [Required]
        public string WinnerNickName { get; set; } = string.Empty;
        public TimeOnly BeginningOfGame { get; set; } = TimeOnly.FromDateTime(DateTime.Now);

        public TimeOnly EndingOfGame { get; set; } = TimeOnly.FromDateTime(DateTime.Now);


    }
}
