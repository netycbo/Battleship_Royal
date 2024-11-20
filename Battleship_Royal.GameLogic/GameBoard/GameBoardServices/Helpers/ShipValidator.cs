using Battleship_Royal.GameLogic.GameBoard.GameBoardServices.Helpers.Interfaces;

namespace Battleship_Royal.GameLogic.GameBoard.GameBoardServices.Helpers
{
    public class ShipValidator : IShipValidator
    {
        private Cell[,] _board;
        private const int MaxCellPerShip = 4;
        private List<Ship> _ships;
        public ShipValidator(IGameContext gameContext)
        {
            _board = gameContext.Board ?? throw new ArgumentNullException(nameof(gameContext.Board));
            _ships = gameContext.Ships ?? throw new ArgumentNullException(nameof(gameContext.Ships));
        }
        public void SetBoard(Cell[,] board)
        {
            _board = board;
        }
        public void SetShips(List<Ship> ships)
        {
            _ships = ships;
        }

        public bool AreAdjacentCellsFree(List<(int Row, int Col)> coordinates)
        {

            foreach (var (row, col) in coordinates)
            {
                if (_board[row, col] != null && _board[row, col].HasShip)
                {
                    return false;
                }

                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        if (i == 0 && j == 0) continue; 

                        int newRow = row + i;
                        int newCol = col + j;

                        if (newRow >= 0 && newRow < _board.GetLength(0) && newCol >= 0 && newCol < _board.GetLength(1))
                        {
                            var adjacentCell = _board[newRow, newCol];
                            if (adjacentCell == null || adjacentCell.HasShip)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public bool AreCellsConnected(List<(int Row, int Col)> coordinates)
        {
            coordinates = coordinates.OrderBy(c => c.Row).ThenBy(c => c.Col).ToList();
            for (int i = 1; i < coordinates.Count; i++)
            {
                int rowDiff = Math.Abs(coordinates[i].Row - coordinates[i - 1].Row);
                int colDiff = Math.Abs(coordinates[i].Col - coordinates[i - 1].Col);
                if (rowDiff > 1 || colDiff > 1) return false;
            }
            return true;
        }
        public void ShipsCount(List<(int Row, int Col)> coordinates)
        {
            if (coordinates.Count > MaxCellPerShip)
            {
                throw new Exception($"Cannot place ship with more than {MaxCellPerShip} cells.");
            }

            var shipSizeCounts = new Dictionary<int, int>
            {
                { 4, 1 },
                { 3, 2 },
                { 2, 3 },
                { 1, 4 }
            };

            int shipSize = coordinates.Count;

            int currentCount = _ships.Count(s => s.Segments.Count == shipSize);
            if (shipSizeCounts.ContainsKey(shipSize) && currentCount >= shipSizeCounts[shipSize])
            {
                throw new Exception($"Cannot place more than {shipSizeCounts[shipSize]} ships with {shipSize} cells.");
            }
            foreach (var size in shipSizeCounts.Keys)
            {
                int placedShipsOfSize = _ships.Count(s => s.Segments.Count == size);
                int remainingShipsOfSize = shipSizeCounts[size] - placedShipsOfSize;

                if (remainingShipsOfSize > 0)
                {
                    Console.WriteLine($"- {remainingShipsOfSize} ships of size {size} can still be placed.");
                }
            }
        }
        public bool IsValidPlacement(int row, int col)
        {
            if (row < 0 || row >= 10 || col < 0 || col >= 10 || _board[row, col] == null || _board[row, col].HasShip)
            {                  
                return false;
            }

            return true;
        }

    }
}
