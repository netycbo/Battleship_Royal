namespace Battleship_Royal.GameLogic.GameContext.Interfaces
{
    public interface IGameContext
    {
        Cell[,] Board { get; set; }
        List<Ship> Ships { get; set; }
    }
}