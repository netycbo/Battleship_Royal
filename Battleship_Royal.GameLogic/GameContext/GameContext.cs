using Battleship_Royal.GameLogic.GameBoard.GameBoardServices.Helpers.Interfaces;
using Battleship_Royal.GameLogic.GameContext.Interfaces;

namespace Battleship_Royal.GameLogic.GameContext
{
    public class GameContext : IGameContext
    {
        public Cell[,] HumanPlayerBoard { get; set; }
        public Cell[,] ComputerPlayerBoard { get; set; }
        public List<Ship> Ships { get; set; }

        public GameContext(IBoardInitializer boardInitializer)
        {
            HumanPlayerBoard = boardInitializer.InitializeBoard(10, 10);
            Console.WriteLine("Human player board initialized.");
            ComputerPlayerBoard = boardInitializer.InitializeBoard(10, 10);
            Console.WriteLine("Computer player board initialized.");
            Ships = new List<Ship>();
            Console.WriteLine("GameContext initialized with board and ships - game context.");
           
        }

    }
}
