using Battleship_Royal.GameLogic.GameBoard.GameBoardServices.Helpers.Interfaces;
using Battleship_Royal.GameLogic.GameContext.Interfaces;
using Microsoft.Extensions.Logging;

namespace Battleship_Royal.GameLogic.GameBoard.GameBoardServices.Helpers
{
    public class TurnLogic(IGameContext gameContext, IGameBoardServices gameBoardServices) : ITurnLogic
    {
        private bool _isHumanPlayerTurn = true;
        public void StartGame(int row, int col)
        {
            while (!IsGameOver())
            {
                if (_isHumanPlayerTurn)
                {
                    HumanPlayerTurn(row, col);
                }
                else
                {
                    ComputerPlayerTurn(row, col);
                }
                _isHumanPlayerTurn = !_isHumanPlayerTurn;
            }
            GameOver();
        }
        private void HumanPlayerTurn(int row, int col)
        {
            var computerBoard = gameContext.ComputerPlayerBoard;
            gameBoardServices.Attack(row, col, computerBoard);

        }
        private void ComputerPlayerTurn(int row, int col)
        {
            var humanBoard = gameContext.HumanPlayerBoard;
            gameBoardServices.Attack(row, col, humanBoard);
        }
        private bool IsGameOver()
        {
            return AllShipsSunk(gameContext.HumanPlayerBoard) || AllShipsSunk(gameContext.ComputerPlayerBoard);
        }
        private bool AllShipsSunk(Cell[,] board)
        {
            int maxHits = 20;
            if (gameBoardServices.GetHitsCount(board) == maxHits)
            {
                return true;
            }
            return false;
        }
        private void GameOver()
        {

            if (AllShipsSunk(gameContext.HumanPlayerBoard))
            {
                Console.WriteLine("Computer player wins!");
            }
            else
            {
                Console.WriteLine("Human player wins!");
            }
        }


    }
}
