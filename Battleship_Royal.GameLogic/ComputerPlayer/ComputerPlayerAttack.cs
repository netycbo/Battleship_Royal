using Battleship_Royal.GameLogic.ComputerPlayer.Interfaces;
using Battleship_Royal.GameLogic.GameContext.Interfaces;


namespace Battleship_Royal.GameLogic.ComputerPlayer
{
    public class ComputerPlayerAttack(IGameContext gameContext) : IComputerPlayerAttack
    {
        public bool ComputerAttack(int row, int col, Cell[,] board)
        {
            const int maxTriesPerTur = 2;
            int tries = 0;

            while (tries <= maxTriesPerTur)
            {
                if (board[row, col].HasShip)
                {
                    var hitShip = gameContext.Ships.FirstOrDefault(ship => ship.Segments.Any(segment => segment == board[row, col]));

                    if (hitShip != null && hitShip.IsSunk())
                    {
                        Console.WriteLine("Statek został zatopiony!");
                    }
                    else
                    {
                        Console.WriteLine("Statek został trafiony!");
                    }
                    return true;

                }
                tries++;

            }
            return false;
        }
    }
}
