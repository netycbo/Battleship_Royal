

using Battleship_Royal.GameLogic.GameBoard.GameBoardServices;

namespace Battleship_Royal.GameLogic
{
    public class SetGameBoard()
    {
        private IGameBoardServices gameBoardServices;
        private Cell[,] board;
        private List<Ship> ships;

        public SetGameBoard(IGameBoardServices gameBoardServices) : this()
        {
            board = new Cell[10, 10];
            ships = new List<Ship>();
            gameBoardServices = gameBoardServices; 
            InitializeBoard();
        }

        public Cell[,] Board => board;

        private void InitializeBoard()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    board[i, j] = new Cell();
                }
            }
        }

        public void PlaceShip(List<(int Row, int Col)> coordinates)
        {
            if (GetShipsCount() + coordinates.Count > 40)
            {
                throw new Exception("Exceeded maximum number of ship cells (40).");
            }

            gameBoardServices.PlaceShip(coordinates); // Delegate to GameBoardServices
        }

        public bool Attack(int row, int col)
        {
            return gameBoardServices.Attack(row, col); // Delegate to GameBoardServices
        }

        public bool IsValidPlacement(int row, int col)
        {
            return gameBoardServices.IsValidPlacement(row, col); // Delegate to GameBoardServices
        }

        public int GetShipsCount()
        {
            return gameBoardServices.GetShipsCount(); // Delegate to GameBoardServices
        }

        public int GetHitsCount()
        {
            return gameBoardServices.GetHitsCount(); // Delegate to GameBoardServices
        }
    }
}
