using static Battleship_Royal.GameLogic.ComputerPlayer.DifficultyLevels.DifficultyServices.BfsAlgorithm;

namespace Battleship_Royal.GameLogic.ComputerPlayer.DifficultyLevels.DifficultyServices.Interfaces
{
    public interface IBfsAlgorithm
    {
        Target BFS(int start);
    }
}