using Battleship_Royal.GameLogic.GameBoard.GameBoardServices.Helpers.Interfaces;
using Battleship_Royal.GameLogic.GameContext.Interfaces;


namespace Battleship_Royal.GameLogic.GameContext
{
    public class GameContext :  IGameContext
    {
        private readonly IBoardInitializer _boardInitializer;
        public Cell[,] HumanPlayerBoard { get;  set; }
        public Cell[,] ComputerPlayerBoard { get; set; }
        public List<Ship> HumanPlayerShips { get;  set; } = new();
        public List<Ship> ComputerPlayerShips { get; set; } = new();

        private bool _isHumanBoardInitialized = false;
        private bool _isComputerBoardInitialized = false;
        public GameContext(IBoardInitializer boardInitializer)
        {
            _boardInitializer = boardInitializer;
        }
        public async Task InitializeHumanPlayerBoard()
        {
            if (_isHumanBoardInitialized) return;
            HumanPlayerBoard = _boardInitializer.InitializeBoard(10, 10);
            Console.WriteLine("Human player board initialized.");
            _isHumanBoardInitialized = true;
        }
        public async Task InitializeComputerPlayerBoard()
        {
            if (_isComputerBoardInitialized) return;
            ComputerPlayerBoard = _boardInitializer.InitializeBoard(10, 10);
            Console.WriteLine("Computer player board initialized.");
            _isComputerBoardInitialized = true;
        }
        public Cell[,] ReadyHumanPlayerBoard()
        {
            if(HumanPlayerBoard == null)
            {
                InitializeHumanPlayerBoard();
            }
           return HumanPlayerBoard;
        }
        public Cell[,] ReadyComputerPlayerBoard()
        {
            if (ComputerPlayerBoard == null)
            {
                InitializeComputerPlayerBoard();
            }
            return ComputerPlayerBoard;
        }
    }
}
