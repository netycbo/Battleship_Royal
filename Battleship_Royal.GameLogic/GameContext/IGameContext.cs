namespace Battleship_Royal.GameLogic.GameContext
{
    public interface IGameContext
    {
        Cell[,] Board { get; set; }
        List<Ship> Ships { get; set; }
    }
}