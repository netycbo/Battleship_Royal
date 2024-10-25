
namespace Battleship_Royal.GameLogic.GameBoard.GameBoardServices
{
    public interface IGameBoardServices
    {
        Cell[,] Board { get; }

        bool Attack(int row, int col);
        int GetHitsCount();
        int GetShipsCount();
        bool IsValidPlacement(int row, int col);
        bool AreAdjacentCellsFree(List<(int Row, int Col)> coordinates);
        void PlaceShip(List<(int Row, int Col)> coordinates);
        void ShipValidation(List<(int Row, int Col)> coordinates);
        bool HasConnectedFour();
    }
}