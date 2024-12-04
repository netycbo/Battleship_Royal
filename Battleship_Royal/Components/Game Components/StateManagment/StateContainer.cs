namespace Battleship_Royal.Components.Game_Components
{
    public class StateContainer : IStateContainer
    {
        private string playerNickName;
        private int difficultyLevel;
        private int timer;
        public string PlayerNickName
        {
            get => playerNickName;
            set
            {
                playerNickName = value;
                NotifyStateChanged();
            }
        }
        public int DifficultyLevel
        {
            get => difficultyLevel;
            set
            {
                difficultyLevel = value;
                NotifyStateChanged();
            }
        }
        public int Timer
        {
            get => timer;
            set
            {
                timer = value;
                NotifyStateChanged();
            }
        }

        public event Action? OnStateChange;

        private void NotifyStateChanged() => OnStateChange?.Invoke();
    }
}
