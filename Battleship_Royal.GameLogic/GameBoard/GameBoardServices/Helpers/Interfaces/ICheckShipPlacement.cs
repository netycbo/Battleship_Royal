namespace Battleship_Royal.GameLogic.GameBoard.GameBoardServices.Helpers.Interfaces
{
    public interface ICheckShipPlacement
    {
        void ValidateShipPlacement(List<(int Row, int Col)> coordinates, List<Ship> existingShips);
    }
}