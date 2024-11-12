using Battleship_Royal.GameLogic.GameBoard.GameBoardServices;
using Battleship_Royal.GameLogic.GameBoard.GameBoardServices.Helpers;
using Battleship_Royal.GameLogic.GameBoard.GameBoardServices.Helpers.Interfaces;


namespace Battleship_Royal.GameLogic
{
    public class SetGameBoard
    {
        private readonly IGameBoardServices gameBoardServices;
        private readonly IShipValidator shipValidator;
        private readonly Cell[,] board;
        private readonly List<Ship> ships;

        public SetGameBoard(IGameBoardServices gameBoardServices, IBoardInitializer boardInitializer, IShipValidator shipValidator)
        {
            
            this.board = boardInitializer.InitializeBoard(10, 10);
            this.ships = new List<Ship>();
            this.gameBoardServices = gameBoardServices;
            this.shipValidator = shipValidator;
        }
        public Cell[,] Board => board;
           
        public void PlaceShip(List<(int Row, int Col)> coordinates)
        {
            foreach(var coordinate in coordinates)
            {
                if(!IsValidPlacement(coordinate.Col, coordinate.Row))
                {
                    throw new Exception("invalid placment");
                }
            }
            if (GetShipsCount() + coordinates.Count > 35)
            {
                throw new Exception("Exceeded maximum number of ship cells (35).");
            }

            gameBoardServices.PlaceShip(coordinates); 
        }

        //public bool Attack(int row, int col)
        //{
        //    return gameBoardServices.Attack(row, col); 
        //}

        public bool IsValidPlacement(int row, int col)
        {
            return shipValidator.IsValidPlacement(row, col);
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
