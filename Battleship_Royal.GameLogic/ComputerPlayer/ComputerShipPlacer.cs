using Battleship_Royal.GameLogic.ComputerPlayer.Interfaces;
using Battleship_Royal.GameLogic.GameBoard.GameBoardServices.Helpers.Interfaces;
using Battleship_Royal.GameLogic.GameBoard.GameBoardServices.Helpers;
using Battleship_Royal.GameLogic.GameContext.Interfaces;
using Battleship_Royal.GameLogic;
using Microsoft.Extensions.DependencyInjection;

public class ComputerShipPlacer : IComputerShipPlacer
{
    private readonly IGameContext _gameContext;
    private readonly IShipValidator _shipValidator;
    private readonly IHasDifferentShape _shapeChecker;
    private readonly Random _random;
    
   

    public ComputerShipPlacer(
        IGameContext gameContext,
        IShipValidator shipValidator,
        IHasDifferentShape shapeChecker)
    {
        _gameContext = gameContext ?? throw new ArgumentNullException(nameof(gameContext));
        _shipValidator = shipValidator ?? throw new ArgumentNullException(nameof(shipValidator));
        _shapeChecker = shapeChecker ?? throw new ArgumentNullException(nameof(shapeChecker));
       
        _random = new Random();
        
    }

    public IGameContext PlaceShipsForComputer()
    {

        var computerBoard = _gameContext.ComputerPlayerBoard;
        _shipValidator.SetBoard(computerBoard);
        _shipValidator.SetShips(_gameContext.ComputerPlayerShips);

        var shipSizes = new List<int> { 4, 3, 3, 2, 2, 2, 1, 1, 1, 1 };

        foreach (int size in shipSizes)
        {
            bool placed = false;
            while (!placed)
            {
                int startRow = _random.Next(0, 10);
                int startCol = _random.Next(0, 10);
                bool isHorizontal = _random.Next(2) == 0;

                var coordinates = GenerateShipCoordinates(startRow, startCol, size, isHorizontal);

                if (coordinates.Count == size &&
                    _shipValidator.AreAdjacentCellsFree(coordinates, _gameContext.ComputerPlayerBoard) &&
                    !_shapeChecker.IsSquareShape(coordinates) &&
                    _shipValidator.IsValidPlacement(startRow, startCol))
                {
                    PlaceShipOnBoard(coordinates, _gameContext.ComputerPlayerBoard, _gameContext.ComputerPlayerShips);
                    placed = true;
                }
            }
        }

        return _gameContext;
    }

    private List<(int Row, int Col)> GenerateShipCoordinates(int startRow, int startCol, int size, bool isHorizontal)
    {
        var coordinates = new List<(int Row, int Col)>();
        for (int i = 0; i < size; i++)
        {
            int row = startRow + (isHorizontal ? 0 : i);
            int col = startCol + (isHorizontal ? i : 0);
            if (row >= 10 || col >= 10) 
                break;
            coordinates.Add((row, col));
        }
        return coordinates;
    }

    private void PlaceShipOnBoard(List<(int Row, int Col)> coordinates, Cell[,] board, List<Ship> ships)
    {
        List<Cell> segments = coordinates.Select(c => board[c.Row, c.Col]).ToList();
        var newShip = new Ship(segments, coordinates);

        foreach (var cell in segments)
        {
            cell.HasShip = true;
            cell.OccupyingShip = newShip;
        }

        ships.Add(newShip);
        Console.WriteLine("Computer placed ship successfully.");
    }
}
