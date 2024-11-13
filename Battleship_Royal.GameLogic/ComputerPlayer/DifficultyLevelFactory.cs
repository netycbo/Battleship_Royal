using Battleship_Royal.GameLogic.ComputerPlayer.DifficultyLevels;
using Battleship_Royal.GameLogic.ComputerPlayer.DifficultyLevels.DifficultyServices.Interfaces;
using Battleship_Royal.GameLogic.ComputerPlayer.DifficultyLevels.DifficultyServices;
using Battleship_Royal.GameLogic.ComputerPlayer.Interfaces;

using Battleship_Royal.GameLogic.GameBoard.GameBoardServices;
using Battleship_Royal.GameLogic.GameBoard.GameBoardServices.Helpers.Interfaces;
using Battleship_Royal.GameLogic.GameBoard.GameBoardServices.Helpers;

namespace Battleship_Royal.GameLogic.ComputerPlayer
{
    public class DifficultyLevelFactory
    {
        Random random;
        IShipValidator shipValidator;
        IGameBoardServices gameBoard;
        IBfsAlgorithm bfs;
        IGenerateRandomCoordinates generateCoordinates;
        IBoardInitializer boardInitializer;
        IShipPlacer shipPlacer;

        public DifficultyLevelFactory()
        {
            random = new Random();
            gameBoard = new GameBoardServices(boardInitializer, shipPlacer, shipValidator);
            bfs = new BfsAlgorithm(gameBoard);
            generateCoordinates = new GenerateRandomCoordinates(random);
        }

        public IDifficultyStrategy GetStrategy(int difficultyLevel)
        {
            return difficultyLevel switch
            {
                1 => new EasyLevel(random, bfs, gameBoard, generateCoordinates),
                2 => new MediumLevel(random, bfs, gameBoard, generateCoordinates),
                3 => new HardLevel(random, bfs, gameBoard, generateCoordinates),
                _ => throw new ArgumentException("Invalid difficulty level")
            };
        }
    }
}
