namespace Battleship_Royal.GameLogic.GameBoard.GameBoardServices
{
    public class GameBoardServices : IGameBoardServices
    {
        private const int MaxShips = 15;
        private const int MaxCellPerShip = 5;
        private Cell[,] board;
        private List<Ship> ships = new List<Ship>();

        public GameBoardServices()
        {
            board = new Cell[10, 10];
            ships = new List<Ship>();
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
            ShipValidation(coordinates);

            List<Cell> segments = new List<Cell>();
            if (coordinates.Count >= 4 && HasConnectedFour())
            {
                throw new Exception("Cannot place a squer ship!");
            }
            foreach (var (row, col) in coordinates)
            {
                if (!IsValidPlacement(row, col))
                {
                    throw new Exception($"Invalid placement at ({row}, {col}).");
                }

                if (ships.Count >= MaxShips)
                {
                    throw new Exception("Too many ships on the board");
                }

                var cell = board[row, col];
                cell.HasShip = true;
                segments.Add(cell);
            }

            var newShip = new Ship(segments, coordinates);

            foreach (var cell in segments)
            {
                cell.OccupyingShip = newShip;
            }

            ships.Add(newShip);
        }

        public bool Attack(int row, int col)
        {
            if (board[row, col].IsHit)
            {
                return false;
            }

            board[row, col].IsHit = true;

            if (board[row, col].HasShip)
            {
                Ship hitShip = ships.FirstOrDefault(ship => ship.Segments.Any(segment => segment == board[row, col]));

                if (hitShip != null && hitShip.IsSunk())
                {

                    Console.WriteLine("Statek został zatopiony!");
                }
                return true;
            }
            return false;
        }

        public bool IsValidPlacement(int row, int col)
        {
            if (row < 0 || row >= 10 || col < 0 || col >= 10 || board[row, col].HasShip)
            {
                return false;
            }

            return true;
        }
        public int GetShipsCount()
        {
            int count = 0;
            foreach (var cell in board)
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
            foreach (var cell in board)
            {
                if (cell != null && cell.IsHit)
                {
                    count++;
                }
            }
            return count;
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

        public void ShipValidation(List<(int Row, int Col)> coordinates)
        {
            if (coordinates.Count > MaxCellPerShip)
            {
                throw new Exception($"Cannot place ship with more than {MaxCellPerShip} cells");
            }

            var shipSizeCounts = new Dictionary<int, int>
            {
                { 5, 1 },
                { 4, 2 },
                { 3, 3 },
                { 2, 4 },
                { 1, 5 }  // max 5 ships with size of 1 cell
            };

            int shipSize = coordinates.Count;
            int currentCount = 0;

            foreach (var ship in ships)
            {
                if (ship.Segments.Count == shipSize)
                {
                    currentCount++;
                }
            }

            if (shipSizeCounts.ContainsKey(shipSize) && currentCount >= shipSizeCounts[shipSize])
            {
                throw new Exception($"Cannot place more than {shipSizeCounts[shipSize]} ships with {shipSize} cells");
            }
            else
            {
                Console.WriteLine("Ships available to place:");
                foreach (var size in shipSizeCounts.Keys)
                {
                    int placedShipsOfSize = ships.Count(s => s.Segments.Count == size);
                    int remainingShipsOfSize = shipSizeCounts[size] - placedShipsOfSize;

                    if (remainingShipsOfSize > 0)
                    {
                        Console.WriteLine($"- {remainingShipsOfSize} ships of size {size}");
                    }
                }
            }
        }
        public bool HasConnectedFour()
        {

            for (int r = 0; r < 10; r++)
            {
                int consecutiveShips = 0;
                for (int c = 0; c < 10; c++)
                {
                    if (board[r, c].HasShip)
                    {
                        consecutiveShips++;
                    }
                    else
                    {
                        if (consecutiveShips > 5) // Zwraca błąd tylko, jeśli jest więcej niż 5 połączone statki
                        {
                            return true;
                        }
                        consecutiveShips = 0;
                    }
                }
                if (consecutiveShips > 5) return true; // Sprawdzenie na końcu wiersza
            }

            // Sprawdzenie pionowe
            for (int c = 0; c < 10; c++)
            {
                int consecutiveShips = 0;
                for (int r = 0; r < 10; r++)
                {
                    if (board[r, c].HasShip)
                    {
                        consecutiveShips++;
                    }
                    else
                    {
                        if (consecutiveShips > 5)
                        {
                            return true;
                        }
                        consecutiveShips = 0;
                    }
                }
                if (consecutiveShips > 5) return true; 
            }

            // Sprawdzenie kwadratów 2x2
            for (int r = 0; r < 9; r++)
            {
                for (int c = 0; c < 9; c++)
                {
                    if (board[r, c].HasShip &&
                        board[r, c + 1].HasShip &&
                        board[r + 1, c].HasShip &&
                        board[r + 1, c + 1].HasShip)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

    }

}

