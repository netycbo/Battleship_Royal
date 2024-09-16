namespace Battleship_Royal.Api.Dtos.Game
{
    public class RematchDto
    {
        public string Player1 { get; set; }
        public string Player2 { get; set; } = string.Empty;
        public string GameWinner { get; set; }
        public bool IsSpeedGame { get; set; } = false;
        public int Timer { get; set; } = 0;
    }
}
