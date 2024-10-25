using Battleship_Royal.GameLogic.ComputerPlayer.Interfaces;

namespace Battleship_Royal.GameLogic.GameBoard.GameBoardServices
{
    public class ComputerShipPlacer(IGameBoardServices gameBoardServices) : IComputerShipPlacer
    {
        public void PlaceShipsForComputer()
        {
            var shipSizes = new List<int> { 5, 4, 3, 3, 2, 1 };

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
                        gameBoardServices.AreAdjacentCellsFree(coordinates) &&
                        !gameBoardServices.HasConnectedFour())
                    {
                        gameBoardServices.PlaceShip(coordinates);
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
                int row = isHorizontal ? startRow : startRow + i;
                int col = isHorizontal ? startCol + i : startCol;

                if (row < 0 || row >= 10 || col < 0 || col >= 10 || !gameBoardServices.IsValidPlacement(row, col))
                {
                    coordinates.Clear();
                    break;
                }
                coordinates.Add((row, col));
            }
            return coordinates;
        }
    }
}
