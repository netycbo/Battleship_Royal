namespace Battleship_Royal.GameLogic.GameBoard.GameBoardServices.Helpers.Interfaces
{
    public interface IShipPlacer
    {
        void PlaceShip(List<(int Row, int Col)> coordinates);
        void SetBoard(Cell[,] board);


    }
}