namespace Battleship_Royal.GameLogic.GameBoard.GameBoardServices.Helpers.Interfaces
{
    public interface IBoardInitializer
    {
        Cell[,] InitializeBoard(int rows, int columns);
    }
}