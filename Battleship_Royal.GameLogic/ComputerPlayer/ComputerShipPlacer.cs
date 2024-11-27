using Battleship_Royal.GameLogic.ComputerPlayer.Interfaces;
using Battleship_Royal.GameLogic.GameBoard.GameBoardServices.Helpers.Interfaces;
using Battleship_Royal.GameLogic.GameBoard.GameBoardServices.Helpers;
using Battleship_Royal.GameLogic.GameBoard.GameBoardServices;
using Battleship_Royal.GameLogic;
using Battleship_Royal.GameLogic.GameContext.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

public class ComputerShipPlacer : IComputerShipPlacer
{
    private readonly IGameContextFactory _gameContextFactory;
    private readonly IShipValidator _shipValidator;
    private readonly IHasDifferentShape _shapeChecker;
    private readonly Random _random;

    public ComputerShipPlacer(IGameContextFactory gameContextFactory, IShipValidator shipValidator, IHasDifferentShape shapeChecker)
    {
        _gameContextFactory = gameContextFactory ?? throw new ArgumentNullException(nameof(gameContextFactory));
        _shipValidator = shipValidator ?? throw new ArgumentNullException(nameof(shipValidator));
        _shapeChecker = shapeChecker ?? throw new ArgumentNullException(nameof(shapeChecker));
        _random = new Random();
    }

    public IGameContext PlaceShipsForComputer()
    {
        var shipSizes = new List<int> { 4, 3, 3, 2, 2, 2, 1, 1, 1, 1 };

        var computerGameContext = _gameContextFactory.CreateGameContext();
        _shipValidator.SetBoard(computerGameContext.Board);
        _shipValidator.SetShips(computerGameContext.Ships);

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
                    _shipValidator.AreAdjacentCellsFree(coordinates, computerGameContext.Board) &&
                    !_shapeChecker.IsSquareShape(coordinates) &&
                    _shipValidator.IsValidPlacement(startRow, startCol))
                {
                    PlaceShipOnBoard(coordinates, computerGameContext.Board, computerGameContext.Ships);
                    placed = true;
                }
            }
        }

        return computerGameContext;
    }

    private List<(int Row, int Col)> GenerateShipCoordinates(int startRow, int startCol, int size, bool isHorizontal)
    {
        var coordinates = new List<(int Row, int Col)>();
        for (int i = 0; i < size; i++)
        {
            int row = startRow + (isHorizontal ? 0 : i);
            int col = startCol + (isHorizontal ? i : 0);
            if (row >= 10 || col >= 10) // Ensure coordinates stay within bounds.
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
