namespace Battleship_Royal.GameLogic
{
    public class GameBoard
    {
        private Cell[,] board;
        private List<Ship> ships;

        public GameBoard()
        {
            board = new Cell[10, 10];
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    board[i, j] = new Cell();
                }
            }
        }

        public void PlaceShip(int startRow, int startCol, int length, bool isVertical)
        {
            List<Cell> segments = new List<Cell>();

            for (int i = 0; i < length; i++)
            {
                int row = isVertical ? startRow + i : startRow;
                int col = isVertical ? startCol : startCol + i;

                if (IsValidPlacement(row, col))
                {
                    segments.Add(board[row, col]);
                }
                else
                {
                    throw new Exception("Invalid placement.");
                }
            }

            Ship newShip = new Ship(segments);
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

        private bool IsValidPlacement(int row, int col)
        {
            // współrzędne są w obrębie planszy?
            if (row < 0 || row >= 10 || col < 0 || col >= 10)
            {
                return false;
            }

            // czy pole jest już zajęte
            if (board[row, col].HasShip)
            {
                return false;
            }

            // Sprawdź sąsiednie pola
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (row + i >= 0 && row + i < 10 && col + j >= 0 && col + j < 10)
                    {
                        if (board[row + i, col + j].HasShip)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
    }
}
