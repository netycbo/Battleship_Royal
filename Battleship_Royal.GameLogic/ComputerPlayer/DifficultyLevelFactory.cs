using Battleship_Royal.GameLogic.ComputerPlayer.DifficultyLevels;
using Battleship_Royal.GameLogic.ComputerPlayer.DifficultyLevels.DifficultyServices.Interfaces;
using Battleship_Royal.GameLogic.ComputerPlayer.DifficultyLevels.DifficultyServices;
using Battleship_Royal.GameLogic.ComputerPlayer.Interfaces;

namespace Battleship_Royal.GameLogic.ComputerPlayer
{
    public class DifficultyLevelFactory
    {
        Random random;
        IGameBoard gameBoard;
        IBfsAlgorithm bfs;
        IGenerateRandomCoordinates generateCoordinates;

        public DifficultyLevelFactory()
        {
            random = new Random(); // Create a new instance of Random
            gameBoard = new GameBoard();
            bfs = new BfsAlgorithm(gameBoard); // Create a new instance of IBfsAlgorithm
            generateCoordinates = new GenerateRandomCoordinates(random); // Create a new instance of IGenerateRandomCoordinates
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
