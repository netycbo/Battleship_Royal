

using Battleship_Royal.GameLogic.GameBoard.GameBoardServices;
using System.Collections.Generic;

namespace Battleship_Royal.GameLogic
{
    public class SetGameBoard
    {
        private IGameBoardServices gameBoardServices;
        private Cell[,] board;
        private List<Ship> ships;

        public SetGameBoard(IGameBoardServices gameBoardServices) 
        {
            board = gameBoardServices.Board;
            ships = new List<Ship>();
            this.gameBoardServices = gameBoardServices; 
            
        }

        public Cell[,] Board => board;
           
        public void PlaceShip(List<(int Row, int Col)> coordinates)
        {
            if (GetShipsCount() + coordinates.Count > 35)
            {
                throw new Exception("Exceeded maximum number of ship cells (40).");
            }

            gameBoardServices.PlaceShip(coordinates); 
        }

        public bool Attack(int row, int col)
        {
            return gameBoardServices.Attack(row, col); 
        }

        public bool IsValidPlacement(int row, int col)
        {
            return gameBoardServices.IsValidPlacement(row, col);
        }

        public int GetShipsCount()
        {
            return gameBoardServices.GetShipsCount(); 
        }

        public int GetHitsCount()
        {
            return gameBoardServices.GetHitsCount(); 
        }
    }
}
