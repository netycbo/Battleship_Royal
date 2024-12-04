
namespace Battleship_Royal.Components.Game_Components
{
    public interface IStateContainer
    {
        int DifficultyLevel { get; set; }
        string PlayerNickName { get; set; }
        int Timer { get; set; }

        event Action? OnStateChange;
    }
}