
namespace Battleship_Royal.GameLogic
{
    public interface ISetGameBoard
    {
        Cell GetCell(int row, int col);
        int GetHitsCount();
        int GetShipsCount();
        void PlaceShip(List<(int Row, int Col)> coordinates);
    }
}