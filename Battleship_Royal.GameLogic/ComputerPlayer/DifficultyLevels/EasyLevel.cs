using Battleship_Royal.GameLogic.ComputerPlayer.DifficultyLevels.DifficultyServices.Interfaces;
using Battleship_Royal.GameLogic.ComputerPlayer.Interfaces;
using Battleship_Royal.GameLogic.GameBoard;
using Battleship_Royal.GameLogic.GameBoard.GameBoardServices;
using Battleship_Royal.GameLogic.GameContext.Interfaces;


namespace Battleship_Royal.GameLogic.ComputerPlayer.DifficultyLevels
{
    public class EasyLevel(Random random, IBfsAlgorithm bfs, IGameBoardServices gameBoard, IGenerateRandomCoordinates generateCoordinates, IGameContext gameContext) : IDifficultyStrategy
    {
        public int BfsAlgorithm()
        {
            var board = gameContext.HumanPlayerBoard;
            BfsAlgorithm.Target target = bfs.BFS(0,0);
            gameBoard.Attack(target.Row, target.Col, board);
            return target.Row * 10 + target.Col;
        }

        public int MakeMove()
        {
            int randomInt = random.Next(1, 101);

            if (randomInt > 10)
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
            var board = gameContext.HumanPlayerBoard;
            generateCoordinates.GenerateCoordinates(out int row, out int col);
            gameBoard.Attack(row, col, board);
            return row * 10 + col;
        }
    }
}
