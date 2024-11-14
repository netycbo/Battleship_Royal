using Battleship_Royal.GameLogic.GameBoard.GameBoardServices.Helpers.Interfaces;

namespace Battleship_Royal.GameLogic.GameBoard.GameBoardServices.Helpers
{
    public class CheckShipPlacement(IHasDifferentShape shapeChecker, IShipValidator shipValidator) : ICheckShipPlacement
    {
        public void ValidateShipPlacement(List<(int Row, int Col)> coordinates, List<Ship> existingShips)
        {
            if (coordinates.Count > 4)
                throw new Exception("Cannot place ship with more than 4 cells.");

            if (coordinates.Count == 4 && shapeChecker.IsSquareShape(coordinates))
            {
                throw new Exception("Cannot place square ship.");
            }
            if (!shipValidator.AreCellsConnected(coordinates))
                throw new Exception("There must be no gaps between selected cells.");
            
            shipValidator.SetShips(existingShips);

            shipValidator.ShipsCount(coordinates);
        }
    }
}
