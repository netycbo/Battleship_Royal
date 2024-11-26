
namespace Battleship_Royal.GameLogic.GameBoard.GameBoardServices.Helpers
{
    public interface IShipValidator
    {
        bool AreAdjacentCellsFree(List<(int Row, int Col)> coordinates, Cell[,] board);
        bool IsValidPlacement(int row, int col);
        void SetBoard(Cell[,] board);
        void SetShips(List<Ship> ships);
        bool AreCellsConnected(List<(int Row, int Col)> coordinates);
        void ShipsCount(List<(int Row, int Col)> coordinates);
    }
}