namespace Battleship_Royal.GameLogic.ComputerPlayer.DifficultyLevels.DifficultyServices.Interfaces
{
    public interface IGenerateRandomCoordinates
    {
        int GenerateCoordinates(out int row, out int col);
    }
}