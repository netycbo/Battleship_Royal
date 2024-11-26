using Battleship_Royal.GameLogic.GameBoard.GameBoardServices.Helpers;
using Battleship_Royal.GameLogic.GameBoard.GameBoardServices.Helpers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship_Royal.GameLogic.GameContext
{
    public class GameContextFactory(IBoardInitializer boardInitializer) : IGameContextFactory
    {
        public GameContext CreateGameContext()
        {
            return new GameContext(boardInitializer);
        }
    }
}
