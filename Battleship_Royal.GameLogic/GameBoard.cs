namespace Battleship_Royal.GameLogic
{
    public class GameBoard : IGameBoard
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

        private bool IsValidPlacement(int row, int col)
        {
            if (row < 0 || row >= 10 || col < 0 || col >= 10 || board[row, col].HasShip)
            {
                return false;
            }

            return true;
        }
    }
}
