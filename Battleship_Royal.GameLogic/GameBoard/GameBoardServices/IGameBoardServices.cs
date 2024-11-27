
namespace Battleship_Royal.GameLogic.GameBoard.GameBoardServices
{
    public interface IGameBoardServices
    {
        void PlaceShip(List<(int Row, int Col)> coordinates);
        int GetShipsCount();
        int GetHitsCount();
        bool Attack(int row, int col, Cell[,] board);

    }
}