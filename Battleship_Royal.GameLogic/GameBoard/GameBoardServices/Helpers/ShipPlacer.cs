using Battleship_Royal.GameLogic.GameBoard.GameBoardServices.Helpers.Interfaces;
using Battleship_Royal.GameLogic.GameBoard.GameBoardServices.Helpers;
using Battleship_Royal.GameLogic;
using Battleship_Royal.GameLogic.GameBoard.GameBoardServices;

public class ShipPlacer : IShipPlacer
{
   
    private List<Ship> _ships;
    private readonly ICheckShipPlacement _validator;
    private readonly List<Cell> _segments;
    private readonly IBoardInitializer _boardInitializer;

    public ShipPlacer( ICheckShipPlacement validator, List<Ship> ships, List<Cell> segments, IBoardInitializer boardInitializer)
    {
        _validator = validator;
       
        _ships = ships ?? new List<Ship>();
        _segments = segments ?? new List<Cell>();
        _boardInitializer = boardInitializer;
        
    }
    
    public void SetShips(List<Ship> ships)
    {
        _ships = ships;
    }

    public void PlaceShip(List<(int Row, int Col)> coordinates)
    {
       var board = _boardInitializer.InitializeBoard(10, 10);
        _validator.ValidateShipPlacement(coordinates, _ships);

        List<Cell> segments = coordinates.Select(c =>
        {
            var cell = board[c.Row, c.Col];
            if (cell == null)
            {
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
        OnShipPlaced?.Invoke(this, EventArgs.Empty);
    }

    public event EventHandler OnShipPlaced;
}
