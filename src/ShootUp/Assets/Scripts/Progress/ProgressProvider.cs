using SettingsModule;

namespace Progress
{
    public class ProgressProvider
    {
        private readonly GameSettings _gameSettings;

        public int CurrentLevel { get; private set; } = 0;
        public int TotalEarnedMoney { get; private set; } = 123;

        public bool HasNextLevel => CurrentLevel >= _gameSettings.Levels.Count;

        public ProgressProvider(GameSettings gameSettings)
        {
            _gameSettings = gameSettings;
        }
    }
}