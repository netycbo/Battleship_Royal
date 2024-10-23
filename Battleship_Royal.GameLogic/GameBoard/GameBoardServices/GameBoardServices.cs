namespace Battleship_Royal.GameLogic.GameBoard.GameBoardServices
{
    public class GameBoardServices : IGameBoardServices
    {
        private const int MaxCells = 40;
        private Cell[,] board;
        private List<Ship> ships;
        public void PlaceShip(List<(int Row, int Col)> coordinates)
        {
            List<Cell> segments = new List<Cell>();

            foreach (var (row, col) in coordinates)
            {
                if (!IsValidPlacement(row, col))
                {
                    throw new Exception($"Invalid placement at ({row}, {col}).");
                }

                segments.Add(board[row, col]);
            }

            Ship newShip = new Ship(segments, coordinates);
            ships.Add(newShip);

            foreach (var segment in segments)
            {
                segment.HasShip = true;
            }
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

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    int newRow = row + i;
                    int newCol = col + j;

                    if (newRow >= 0 && newRow < 10 && newCol >= 0 && newCol < 10)
                    {
                        if (board[newRow, newCol].HasShip)
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }
        public int GetShipsCount()
        {
            int count = 0;
            foreach (var cell in board)
            {
                if (cell.HasShip)
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
                if (cell.IsHit)
                {
                    count++;
                }
            }
            return count;
        }
    }
}

