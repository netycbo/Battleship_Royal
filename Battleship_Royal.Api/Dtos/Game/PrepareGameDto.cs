namespace Battleship_Royal.Api.Dtos.Game
{
    public class PrepareGameDto
    {
        public int GameId { get; set; }
        public string Player1 { get; set; }
        public string Player2 { get; set; } = string.Empty;   
        public bool IsSpeedGame { get; set; } = false;
        public int Timer { get; set; } = 10;
        public int DifficultyLevel { get; set; }
    }
}