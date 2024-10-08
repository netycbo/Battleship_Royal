using Microsoft.AspNetCore.Identity;

namespace Battleship_Royal.Data.Entities
{
    public class ComputerPlayer
    {
        public int Id { get; set; }
        public string NickName { get; set; } = "Red October";
        public int DifficultyLevel { get; set; }
    }
}
