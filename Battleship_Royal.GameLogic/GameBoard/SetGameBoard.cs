using Battleship_Royal.GameLogic.GameBoard.GameBoardServices;
using Battleship_Royal.GameLogic.GameBoard.GameBoardServices.Helpers;
using Battleship_Royal.GameLogic.GameBoard.GameBoardServices.Helpers.Interfaces;


namespace Battleship_Royal.GameLogic
{
    public class SetGameBoard : ISetGameBoard
    {
        private readonly Cell[,] _board;
        private readonly IShipValidator _shipValidator;
        private readonly IGameBoardServices? gameBoardServices;

        public SetGameBoard(IGameContext gameContext, IShipValidator shipValidator)
        {
            _board = gameContext.Board ?? throw new ArgumentNullException(nameof(gameContext.Board));
            _shipValidator = shipValidator;
        }

        public void PlaceShip(List<(int Row, int Col)> coordinates)
        {
            //foreach(var coordinate in coordinates)
            //{
            //    if(!IsValidPlacement(coordinate.Col, coordinate.Row))
            //    {
            //        throw new Exception("invalid placment");
            //    }
            //}
            if (GetShipsCount() + coordinates.Count > 35)
            {
                throw new Exception("Exceeded maximum number of ship cells (35).");
            }

            gameBoardServices.PlaceShip(coordinates);
        }

        //public bool Attack(int row, int col)
        //{
        //    return gameBoardServices.Attack(row, col); 
        //}

        //public bool IsValidPlacement(int row, int col)
        //{
        //    return shipValidator.AreAdjacentCellsFree(new List<(int, int)> { (row, col) });
        //}

        public int GetShipsCount()
        {
            return gameBoardServices.GetShipsCount();
        }

        public int GetHitsCount()
        {
            return gameBoardServices.GetHitsCount();
        }
        public Cell GetCell(int row, int col)
        {
            if (row < 0 || row >= _board.GetLength(0) || col < 0 || col >= _board.GetLength(1))
            {
                throw new ArgumentOutOfRangeException("Podane współrzędne są poza zakresem planszy.");
            }
            return _board[row, col];
        }
    }
}
