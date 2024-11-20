using Battleship_Royal.GameLogic.GameBoard.GameBoardServices.Helpers.Interfaces;

namespace Battleship_Royal.GameLogic.GameBoard.GameBoardServices.Helpers
{
    public class BoardInitializer : IBoardInitializer
    {
        public Cell[,] InitializeBoard(int rows, int cols)
        {
            Console.WriteLine($"Initializing board with dimensions {rows}x{cols}");
            var board = new Cell[rows, cols];
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    board[row, col] = new Cell
                    {
                        Row = row,
                        Col = col,
                        HasShip = false,
                        IsHit = false
                    };
                }
            }
            Console.WriteLine("Board initialized successfully. - initializer");
            return board;
        }
    }
}
