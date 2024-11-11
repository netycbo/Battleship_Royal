using Battleship_Royal.GameLogic.GameBoard.GameBoardServices.Helpers.Interfaces;

namespace Battleship_Royal.GameLogic.GameBoard.GameBoardServices.Helpers
{
    public class BoardInitializer : IBoardInitializer
    {
        public Cell[,] InitializeBoard(int rows, int columns)
        {
            var board = new Cell[rows, columns];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    board[i, j] = new Cell();
                }
            }
            return board;
        }
    }
}
