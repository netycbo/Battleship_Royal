namespace Battleship_Royal.GameLogic.GameContext.Interfaces
{
    public interface IGameContext
    {
        Cell[,] ComputerPlayerBoard { get; }
        List<Ship> ComputerPlayerShips { get; }
        Cell[,] HumanPlayerBoard { get; }
        List<Ship> HumanPlayerShips { get; }
        Task InitializeHumanPlayerBoard();
        Task InitializeComputerPlayerBoard();
        Cell[,] ReadyHumanPlayerBoard();
        Cell[,] ReadyComputerPlayerBoard();
    }
}