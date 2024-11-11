namespace Battleship_Royal.Api.Dtos.Game
{
    public class PrepareGameVsComputerDto
    {
        public string GameId { get; set; } = "dsafsdfsdfraw23423534rdgd";
        public string PlayerNickName { get; set; } = "miki";
        public bool IsSpeedGame { get; set; } = false;
        //public string TimeOfEndGame { get; set; }  
        public int Timer { get; set; } = 10;
        public int DifficultyLevel { get; set; } = 1;
        
    }
}