using Battleship_Royal.GameLogic.GameBoard.GameBoardServices;
using Battleship_Royal.GameLogic.GameBoard.GameBoardServices.Helpers;
using Battleship_Royal.GameLogic.GameBoard.GameBoardServices.Helpers.Interfaces;
using Battleship_Royal.GameLogic.GameContext.Interfaces;


namespace Battleship_Royal.GameLogic
{
    public class SetGameBoard : ISetGameBoard
    {
        private readonly Cell[,] _board;
        private readonly IShipValidator _shipValidator;
        private readonly IGameBoardServices? gameBoardServices;

        public SetGameBoard(IGameContext gameContext, IShipValidator shipValidator)
        {
            _board = gameContext.ReadyHumanPlayerBoard();
            _shipValidator = shipValidator;
        }

        
        public Cell GetCell(int row, int col)
        {
            if (row < 0 || row >= _board.GetLength(0) || col < 0 || col >= _board.GetLength(1))
            {
                throw new ArgumentOutOfRangeException("Podane współrzędne są poza zakresem planszy.");
            }
            return _board[row, col];
        }
    }
}
