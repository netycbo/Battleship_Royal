
namespace Battleship_Royal.GameLogic
{
    public interface IGameContext
    {
        Cell[,] Board { get; set; }
        List<Ship> Ships { get; set; }
    }
}