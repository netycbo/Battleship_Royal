using Battleship_Royal.Data.Entities.Identity;

namespace Battleship_Royal.Data.Entities
{
    public class TemporaryGame
    {
        public ApplicationUser Player1 { get; set; }
        public ApplicationUser Player2 { get; set; }
        public bool IsSpeedGame { get; set; }
        public int Timer { get; set; }
    }
}
