namespace Battleship_Royal.GameLogic.ComputerPlayer.Interfaces
{
    public interface IDifficultyStrategy
    {
        int MakeMove();
        int BfsAlgorithm();
        int RandomMove();
    }
}
