using Battleship_Royal.Data.Services.GameServices.Helpers;
using Battleship_Royal.GameLogic.GameBoard.GameBoardServices.Helpers;
using Battleship_Royal.GameLogic.GameBoard.GameBoardServices.Helpers.Interfaces;


namespace Battleship_Royal.GameLogic.GameBoard.GameBoardServices
{
    public class GameBoardServices : IGameBoardServices
    {

        private readonly IShipValidator _shipValidator;
        private readonly IShipPlacer _shipPlacer;
        private Cell[,] _board;
        private readonly List<Ship> _ships;

        public GameBoardServices(IBoardInitializer boardInitializer, IShipPlacer shipPlacer, IShipValidator shipValidator)
        {
            _board = boardInitializer.InitializeBoard(10, 10);
            _shipPlacer = shipPlacer;
            _shipValidator = shipValidator;
            _ships = new List<Ship>();

            //_shipPlacer.SetBoard(_board);
            _shipValidator.SetBoard(_board);
            _shipPlacer.SetShips(_ships);
            _shipValidator.SetShips(_ships);
        }
        public Cell[,] Board => _board;
        public List<Ship> Ships => _ships;

        public void PlaceShip(List<(int Row, int Col)> coordinates)
        {
            if (_board == null)
            {
                throw new NullReferenceException("Board has not been initialized.");
            }

            foreach (var (row, col) in coordinates)
            {
                if (row < 0 || row >= _board.GetLength(0) || col < 0 || col >= _board.GetLength(1))
                {
                    throw new ArgumentOutOfRangeException($"Invalid coordinate ({row}, {col}). Out of board bounds.");
                }
            }

            //if (_shipValidator.IsValidPlacement(row: coordinates[0].Row, col: coordinates[0].Col))
            //{
            //    throw new Exception("Invalid placement: coordinates do not meet placement rules.");
            //}

            _shipPlacer.PlaceShip(coordinates);
        }

        public bool Attack(int row, int col)
        {
            if (_board[row, col].IsHit)
            {
                return false;
            }

            _board[row, col].IsHit = true;

            if (_board[row, col].HasShip)
            {
                Ship hitShip = _ships.FirstOrDefault(ship => ship.Segments.Any(segment => segment == _board[row, col]));

                if (hitShip != null && hitShip.IsSunk())
                {
                    Console.WriteLine("Statek został zatopiony!");
                }
                return true;
            }
            return false;
        }

        public int GetShipsCount() => _ships.Count;
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

        
    }
}

