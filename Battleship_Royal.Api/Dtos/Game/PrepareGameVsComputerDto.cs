namespace Battleship_Royal.Api.Dtos.Game
{
    public class PrepareGameVsComputerDto
    {
        public string GameId { get; set; }
        public string PlayerNickName { get; set; }
        public bool IsSpeedGame { get; set; } = false;
        public TimeOnly TimeOfEndGame { get; set; }  
        public int Timer { get; set; } = 10;
        public int DifficultyLevel { get; set; }
        
    }
}