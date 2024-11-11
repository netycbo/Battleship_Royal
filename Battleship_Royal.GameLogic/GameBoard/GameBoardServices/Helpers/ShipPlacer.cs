using Battleship_Royal.GameLogic.GameBoard.GameBoardServices.Helpers.Interfaces;

namespace Battleship_Royal.GameLogic.GameBoard.GameBoardServices.Helpers
{
    public class ShipPlacer : IShipPlacer
    {
        private readonly Cell[,] _board;
        private readonly List<Ship> _ships;
        private readonly IShipValidator _validator;

        public ShipPlacer(IBoardInitializer boardInitializer, IShipValidator validator)
        {
            _board = boardInitializer.InitializeBoard(10, 10);
            _ships = new List<Ship>();
            _validator = validator;
        }
        public void PlaceShip(List<(int Row, int Col)> coordinates)
        {
            _validator.ValidateShipPlacement(coordinates, _ships);

            List<Cell> segments = coordinates.Select(c => _board[c.Row, c.Col]).ToList();
            var newShip = new Ship(segments, coordinates);

            foreach (var cell in segments)
            {
                cell.HasShip = true;
                cell.OccupyingShip = newShip;
            }

            _ships.Add(newShip);
            Console.WriteLine($"number of ships: {_ships.Count}");
        }
    }
}
