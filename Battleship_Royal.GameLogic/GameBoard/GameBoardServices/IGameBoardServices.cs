
namespace Battleship_Royal.GameLogic.GameBoard.GameBoardServices
{
    public interface IGameBoardServices
    {
        bool Attack(int row, int col);
        int GetHitsCount();
        int GetShipsCount();
        bool IsValidPlacement(int row, int col);
        void PlaceShip(List<(int Row, int Col)> coordinates);
    }
}