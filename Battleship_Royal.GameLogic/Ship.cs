namespace Battleship_Royal.GameLogic
{
    public class Ship
    {
        public List<Cell> Segments { get; private set; }

        public Ship(List<Cell> segments)
        {
            Segments = segments;
        }

        public bool IsSunk()
        {
            return Segments.All(segment => segment.IsHit);
        }
    }
}
