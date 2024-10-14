namespace Battleship_Royal.GameLogic
{
    public class Ship
    {
        public List<Cell> Segments { get; private set; }
        public List<(int Row, int Col)> Coordinates { get; private set; } // To hold coordinates for non-linear shapes

        public Ship(List<Cell> segments, List<(int Row, int Col)> coordinates)
        {
            Segments = segments;
            Coordinates = coordinates;
        }

        public bool IsSunk()
        {
            return Segments.All(segment => segment.IsHit);
        }
    }
}
