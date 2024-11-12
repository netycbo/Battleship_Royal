using Battleship_Royal.GameLogic.GameBoard.GameBoardServices.Helpers.Interfaces;
using Battleship_Royal.GameLogic.GameBoard.GameBoardServices.Helpers;
using Battleship_Royal.GameLogic;

public class ShipPlacer : IShipPlacer
{
    private Cell[,] _board;
    private readonly List<Ship> _ships;
    private readonly IShipValidator _validator;

    public ShipPlacer(IBoardInitializer boardInitializer, IShipValidator validator)
    {
        _ships = new List<Ship>();
        _validator = validator;
    }
    public void SetBoard(Cell[,] board)
    {
        _board = board;
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
        OnShipPlaced?.Invoke(this, EventArgs.Empty);
    }

    public event EventHandler OnShipPlaced;
}
