using Battleship_Royal.GameLogic.ComputerPlayer.DifficultyLevels.DifficultyServices.Interfaces;
using Battleship_Royal.GameLogic.ComputerPlayer.Interfaces;
using Battleship_Royal.GameLogic.GameBoard;
using Battleship_Royal.GameLogic.GameBoard.GameBoardServices;
using System.ComponentModel;
using static Battleship_Royal.GameLogic.ComputerPlayer.DifficultyLevels.DifficultyServices.BfsAlgorithm;

namespace Battleship_Royal.GameLogic.ComputerPlayer.DifficultyLevels
{
    public class HardLevel(Random random, IBfsAlgorithm bfs, IGameBoardServices gameBoard, IGenerateRandomCoordinates generatCoordinates) : IDifficultyStrategy
    {
        public int BfsAlgorithm()
        {
            Target target = bfs.BFS(0);
            gameBoard.Attack(target.Row, target.Col);
            return target.Row * 10 + target.Col;
        }

        public int MakeMove()
        {
            int randomInt = random.Next(1, 101);

            if (randomInt > 80)
            {
                return BfsAlgorithm();
            }
            else
            {
                return RandomMove();
            }
        }
        public int RandomMove()
        {
            generatCoordinates.GenerateCoordinates(out int row, out int col);
            gameBoard.Attack(row, col);

            return row * 10 + col;
        }


    }
}
