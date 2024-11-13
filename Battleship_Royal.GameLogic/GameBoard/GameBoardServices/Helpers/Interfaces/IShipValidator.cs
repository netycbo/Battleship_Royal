
namespace Battleship_Royal.GameLogic.GameBoard.GameBoardServices.Helpers
{
    public interface IShipValidator
    {
        void ValidateShipPlacement(List<(int Row, int Col)> coordinates, List<Ship> existingShips);
        bool AreAdjacentCellsFree(List<(int Row, int Col)> coordinates);
        bool IsValidPlacement(int row, int col);
        void SetBoard(Cell[,] board);
    }
}