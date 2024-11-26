using Battleship_Royal.GameLogic.ComputerPlayer.Interfaces;
using Battleship_Royal.GameLogic.GameBoard.GameBoardServices.Helpers.Interfaces;
using Battleship_Royal.GameLogic.GameBoard.GameBoardServices.Helpers;
using Battleship_Royal.GameLogic.GameBoard.GameBoardServices;
using Battleship_Royal.GameLogic.GameContext;
using Battleship_Royal.GameLogic;

public class ComputerShipPlacer(IGameContextFactory gameContextFactory, IShipValidator shipValidator, IHasDifferentShape shapeChecker) : IComputerShipPlacer
{
    public void PlaceShipsForComputer()
    {
        var shipSizes = new List<int> { 4, 3, 3, 2, 2, 2, 1, 1, 1, 1 };

        var computerGameContext = gameContextFactory.CreateGameContext();
        shipValidator.SetBoard(computerGameContext.Board);
        shipValidator.SetShips(computerGameContext.Ships);


        foreach (int size in shipSizes)
        {
            bool placed = false;
            while (!placed)
            {
                int startRow = new Random().Next(0, 10);
                int startCol = new Random().Next(0, 10);
                bool isHorizontal = new Random().Next(2) == 0;

                var coordinates = GenerateShipCoordinates(startRow, startCol, size, isHorizontal);

                if (coordinates.Count == size &&
                    shipValidator.AreAdjacentCellsFree(coordinates, computerGameContext.Board) &&
                    !shapeChecker.IsSquareShape(coordinates) &&
                    shipValidator.IsValidPlacement(startRow, startCol))

                {
                    PlaceShipOnBoard(coordinates, computerGameContext.Board, computerGameContext.Ships);
                    placed = true;
                }
            }
        }
    }

    private List<(int Row, int Col)> GenerateShipCoordinates(int startRow, int startCol, int size, bool isHorizontal)
    {
        var coordinates = new List<(int Row, int Col)>();
        for (int i = 0; i < size; i++)
        {
            int row = startRow + (isHorizontal ? 0 : i);
            int col = startCol + (isHorizontal ? i : 0);
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