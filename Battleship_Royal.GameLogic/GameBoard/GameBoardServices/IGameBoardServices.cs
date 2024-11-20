
namespace Battleship_Royal.GameLogic.GameBoard.GameBoardServices
{
    public interface IGameBoardServices
    {
        //Cell[,] Board { get; }

        void PlaceShip(List<(int Row, int Col)> coordinates);
        //void Attack(int row, int col);
        int GetShipsCount();
        int GetHitsCount();
        bool Attack (int row, int col);

    }
}