using Battleship_Royal.GameLogic.GameContext.Interfaces;

namespace Battleship_Royal.GameLogic.ComputerPlayer.Interfaces
{
    public interface IComputerShipPlacer
    {
        IGameContext PlaceShipsForComputer();
    }
}