using Battleship_Royal.GameLogic.GameBoard.GameBoardServices.Helpers.Interfaces;

namespace Battleship_Royal.GameLogic.GameBoard.GameBoardServices.Helpers
{
    public class ShipValidator : IShipValidator
    {
        private const int MaxCellPerShip = 4;
        private Cell[,] Board;
        private readonly IHasDifferentShape shapeChecker;
         private List<Ship> ships = new List<Ship>();
        public ShipValidator(Cell[,] board, IHasDifferentShape shapeChecker)
        {
            this.Board = board;
            this.shapeChecker = shapeChecker;
        }

        public void ValidateShipPlacement(List<(int Row, int Col)> coordinates, List<Ship> existingShips)
        {
            if (coordinates.Count > 4)
                throw new Exception("Cannot place ship with more than 4 cells.");

            if (coordinates.Count == 4 && shapeChecker.IsSquareShape(coordinates))
            {
                throw new Exception("Cannot place square ship.");
            }
            if (!AreCellsConnected(coordinates))
                throw new Exception("There must be no gaps between selected cells.");
        }
        public bool AreAdjacentCellsFree(List<(int Row, int Col)> coordinates)
        {
            foreach (var (row, col) in coordinates)
            {

                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        if (i == 0 && j == 0) continue;

                        int newRow = row + i;
                        int newCol = col + j;
                        if (newRow >= 0 && newRow < 10 && newCol >= 0 && newCol < 10 && Board[newRow, newCol].HasShip)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        private bool AreCellsConnected(List<(int Row, int Col)> coordinates)
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

            int currentCount = ships.Count(s => s.Segments.Count == shipSize);
            if (shipSizeCounts.ContainsKey(shipSize) && currentCount >= shipSizeCounts[shipSize])
            {
                throw new Exception($"Cannot place more than {shipSizeCounts[shipSize]} ships with {shipSize} cells.");
            }
            foreach (var size in shipSizeCounts.Keys)
            {
                int placedShipsOfSize = ships.Count(s => s.Segments.Count == size);
                int remainingShipsOfSize = shipSizeCounts[size] - placedShipsOfSize;

                if (remainingShipsOfSize > 0)
                {
                    Console.WriteLine($"- {remainingShipsOfSize} ships of size {size} can still be placed.");
                }
            }
        }
        public bool IsValidPlacement(int row, int col)
        {
            if (row < 0 || row >= 10 || col < 0 || col >= 10 || Board[row, col].HasShip)
            {
                return false;
            }

            return true;
        }

    }
}
