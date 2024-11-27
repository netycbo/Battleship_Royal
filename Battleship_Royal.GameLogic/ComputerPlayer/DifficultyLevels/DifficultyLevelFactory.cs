using Battleship_Royal.GameLogic.ComputerPlayer.DifficultyLevels.DifficultyServices.Interfaces;
using Battleship_Royal.GameLogic.ComputerPlayer.DifficultyLevels.DifficultyServices;
using Battleship_Royal.GameLogic.ComputerPlayer.Interfaces;
using Battleship_Royal.GameLogic.GameBoard.GameBoardServices;
using Battleship_Royal.GameLogic.GameContext.Interfaces;


namespace Battleship_Royal.GameLogic.ComputerPlayer.DifficultyLevels
{
    public class DifficultyLevelFactory
    {
        Random random;
        IGameBoardServices gameBoard;
        IBfsAlgorithm bfs;
        IGenerateRandomCoordinates generateCoordinates;
        IGameContext gameContext;

        public DifficultyLevelFactory()
        {
            random = new Random();
            bfs = new BfsAlgorithm(gameContext, gameBoard);
            generateCoordinates = new GenerateRandomCoordinates(random);
        }

        public IDifficultyStrategy GetStrategy(int difficultyLevel)
        {
            return difficultyLevel switch
            {
                1 => new EasyLevel(random, bfs, gameBoard, generateCoordinates, gameContext),
                2 => new MediumLevel(random, bfs, gameBoard, generateCoordinates, gameContext),
                3 => new HardLevel(random, bfs, gameBoard, generateCoordinates, gameContext),
                _ => throw new ArgumentException("Invalid difficulty level")
            };
        }
    }
}
