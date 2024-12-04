
namespace Battleship_Royal.GameLogic.GameBoard.GameBoardServices
{
    public interface IGameBoardServices
    {
        void PlaceShip(List<(int Row, int Col)> coordinates);
        int GetShipsCount(List<Ship> ships) => ships.Count;
        int GetHitsCount(Cell[,] board);
        bool Attack(int row, int col, Cell[,] board);
        

    }
}