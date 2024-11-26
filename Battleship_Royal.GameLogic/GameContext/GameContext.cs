using Battleship_Royal.GameLogic.GameBoard.GameBoardServices.Helpers.Interfaces;

namespace Battleship_Royal.GameLogic.GameContext
{
    public class GameContext : IGameContext
    {
        public Cell[,] Board { get; set; }
        public List<Ship> Ships { get; set; }

        public GameContext(IBoardInitializer boardInitializer)
        {
            Board = boardInitializer.InitializeBoard(10, 10);
            Ships = new List<Ship>();
            Console.WriteLine("GameContext initialized with board and ships - game context.");
        }
    }
}
