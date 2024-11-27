namespace Battleship_Royal.GameLogic
{
    public class Cell
    {
        public bool HasShip { get; set; } = false;
        public bool IsHit { get; set; } = false;
        public bool IsSunk { get; set; } = false;
        public Ship? OccupyingShip { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }


    }
}
