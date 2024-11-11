namespace Battleship_Royal.GameLogic.GameBoard.GameBoardServices.Helpers.Interfaces
{
    public interface IHasDifferentShape
    {
        bool DifferentShape(List<(int Row, int Col)> coordinates);
        bool IsSquareShape(List<(int Row, int Col)> coordinates);
    }
}