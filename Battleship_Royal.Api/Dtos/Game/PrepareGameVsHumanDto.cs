namespace Battleship_Royal.Api.Dtos.Game
{
    public class PrepareGameVsHumanDto
    {
        public string GameId { get; set; }

        public string Player1NickName { get; set; }

        public string Player2NickName { get; set; }
        public bool IsSpeedGame { get; set; }
        public int Timer { get; set; }


    }
}