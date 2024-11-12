using Battleship_Royal.Data.Services.GameServices.Helpers;
using Battleship_Royal.GameLogic.GameBoard.GameBoardServices.Helpers;
using Battleship_Royal.GameLogic.GameBoard.GameBoardServices.Helpers.Interfaces;


namespace Battleship_Royal.GameLogic.GameBoard.GameBoardServices
{
    public class GameBoardServices : IGameBoardServices
    {

        
        private readonly IShipPlacer _shipPlacer;
        private Cell[,] _board;

        public GameBoardServices(IBoardInitializer boardInitializer, IShipPlacer shipPlacer)
        {

            _board = boardInitializer.InitializeBoard(10, 10);
            _shipPlacer = shipPlacer;
            _shipPlacer.SetBoard(_board);
        }
        public Cell[,] Board => _board;

        public void PlaceShip(List<(int Row, int Col)> coordinates)
        {
            _shipPlacer.PlaceShip(coordinates);
        }

        //public bool Attack(int row, int col)
        //{
        //    if (_board[row, col].IsHit)
        //    {
        //        return false;
        //    }

        //    _board[row, col].IsHit = true;

        //    if (_board[row, col].HasShip)
        //    {
        //        Ship hitShip = _shipPlacer.Ships.FirstOrDefault(ship => ship.Segments.Any(segment => segment == _board[row, col]));

        //        if (hitShip != null && hitShip.IsSunk())
        //        {

        //            Console.WriteLine("Statek został zatopiony!");
        //        }
        //        return true;
        //    }
        //    return false;
        //}

        public int GetShipsCount()
        {
            int count = 0;
            foreach (var cell in _board)
            {
                if (cell != null && cell.HasShip)
                {
                    count++;
                }
            }
            return count;
        }
        public int GetHitsCount()
        {
            int count = 0;
            foreach (var cell in _board)
            {
                if (cell != null && cell.IsHit)
                {
                    count++;
                }
            }
            return count;
        }

        public bool Attack(int row, int col)
        {
            throw new NotImplementedException();
        }
    }
}

