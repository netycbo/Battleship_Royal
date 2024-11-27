namespace Battleship_Royal.GameLogic.ComputerPlayer.Interfaces
{
    public interface IComputerPlayerAttack
    {
        bool ComputerAttack(int row, int col, Cell[,] board);
    }
}