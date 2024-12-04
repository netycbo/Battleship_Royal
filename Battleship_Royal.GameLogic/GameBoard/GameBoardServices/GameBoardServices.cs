using Battleship_Royal.Data.Services.GameServices.Helpers;
using Battleship_Royal.GameLogic.GameBoard.GameBoardServices.Helpers;
using Battleship_Royal.GameLogic.GameBoard.GameBoardServices.Helpers.Interfaces;
using Battleship_Royal.GameLogic.GameContext;
using Battleship_Royal.GameLogic.GameContext.Interfaces;


namespace Battleship_Royal.GameLogic.GameBoard.GameBoardServices
{
    public class GameBoardServices : IGameBoardServices
    {
        private readonly IGameContext _gameContext;
        private readonly IShipValidator _shipValidator;
        private readonly IShipPlacer _shipPlacer;
        private Cell[,] _board;
        private readonly List<Ship> _ships;

        public GameBoardServices(IGameContext gameContext, IShipPlacer shipPlacer, IShipValidator shipValidator)
        {
            _board = gameContext.HumanPlayerBoard ?? throw new ArgumentNullException(nameof(gameContext.HumanPlayerBoard));
            _ships = gameContext.HumanPlayerShips ?? throw new ArgumentNullException(nameof(gameContext.HumanPlayerShips));
            _shipPlacer = shipPlacer ?? throw new ArgumentNullException(nameof(shipPlacer));
            _shipValidator = shipValidator ?? throw new ArgumentNullException(nameof(shipValidator));
            _gameContext = gameContext;
            Console.WriteLine("GameBoardServices initialized with shared GameContext.");
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

            _shipPlacer.PlaceShip(coordinates);
        }

        public bool Attack(int row, int col, Cell[,] board)
        {
            const int maxTriesPerTur = 2;
            int tries = 0;

            while (tries <= maxTriesPerTur)
            {
                if (board[row, col].HasShip)
                {
                    var hitShip = _gameContext.HumanPlayerShips.FirstOrDefault(ship => ship.Segments.Any(segment => segment == board[row, col]));

                    if (hitShip != null && hitShip.IsSunk())
                    {
                        Console.WriteLine("Statek został zatopiony!");
                    }
                    else
                    {
                        Console.WriteLine("Statek został trafiony!");
                    }
                    return true;
                }
                tries++;
            }
            return false;
        }
        public int GetShipsCount(List<Ship> ships) => ships.Count;
        public int GetHitsCount(Cell[,] board)
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

