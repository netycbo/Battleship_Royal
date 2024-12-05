using Battleship_Royal.GameLogic.GameBoard.GameBoardServices.Helpers.Interfaces;
using Battleship_Royal.GameLogic;

using Battleship_Royal.GameLogic.GameContext.Interfaces;

public class ShipPlacer : IShipPlacer
{
    private List<Ship> _ships;
    private Cell[,] _board;
    private readonly ICheckShipPlacement _validator;
    public ShipPlacer(IGameContext gameContext, ICheckShipPlacement validator)
    {
        _board = gameContext.ReadyHumanPlayerBoard();
        Console.WriteLine("z szip placera");
        _ships = gameContext.HumanPlayerShips ?? throw new ArgumentNullException(nameof(gameContext.HumanPlayerShips));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));

        Console.WriteLine("ShipPlacer initialized with shared GameContext - szip placer.");
    }
    public void PlaceShip(List<(int Row, int Col)> coordinates)
    {

        _validator.ValidateShipPlacement(coordinates, _ships);

        List<Cell> segments = coordinates.Select(c =>
        {
            if (_board == null)
            {
                throw new InvalidOperationException("Board is null.");
            }

            if (c.Row < 0 || c.Row >= _board.GetLength(0) || c.Col < 0 || c.Col >= _board.GetLength(1))
            {
                throw new ArgumentOutOfRangeException($"Coordinates ({c.Row}, {c.Col}) are out of board bounds.");
            }

            var cell = _board[c.Row, c.Col];
            if (cell == null)
            {
                Console.WriteLine($"Cell at ({c.Row}, {c.Col}) is null.");
                throw new InvalidOperationException($"Cell at ({c.Row}, {c.Col}) is null.");
            }

            return cell;
        }).ToList();
        var newShip = new Ship(segments, coordinates);

        foreach (var cell in segments)
        {
            cell.HasShip = true;
            cell.OccupyingShip = newShip;
        }

        _ships.Add(newShip);
        Console.WriteLine($"Ship placed successfully from ShipPlacer: {_ships.Count}.");
        OnShipPlaced?.Invoke(this, EventArgs.Empty);
    }



    public event EventHandler OnShipPlaced;
}