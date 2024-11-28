namespace Battleship_Royal.GameLogic.GameContext.Interfaces
{
    public interface IGameContext
    {
        Cell[,] HumanPlayerBoard { get; set; }

        Cell[,] ComputerPlayerBoard { get; set; }
        List<Ship> Ships { get; set; }
    }
}