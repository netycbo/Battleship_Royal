using Battleship_Royal.Data.Entities.Identity;

namespace Battleship_Royal.Data.Entities
{
    public class TemporaryGame
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public string Player1Id { get; set; }  
        public string Player2Id { get; set; } = string.Empty;
        public bool IsSpeedGame { get; set; }
        public int Timer { get; set; }
    }
}
