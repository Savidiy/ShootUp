using SettingsModule;
using UnityEngine;

namespace Progress
{
    public class ProgressProvider
    {
        private readonly GameSettings _gameSettings;
        private const string PREFS_KEY = "PROGRESS_LEVEL";

        public int CurrentLevel { get; private set; } = 0;

        public bool HasNextLevel => CurrentLevel >= _gameSettings.Levels.Count;

        public ProgressProvider(GameSettings gameSettings)
        {
            _gameSettings = gameSettings;

            CurrentLevel = PlayerPrefs.GetInt(PREFS_KEY, 0);
        }

        public void OpenNextLevel()
        {
            CurrentLevel++;
            PlayerPrefs.SetInt(PREFS_KEY, CurrentLevel);
            PlayerPrefs.Save();

            Debug.Log($"Current Level '{CurrentLevel}'");
        }

        public void ResetProgress()
        {
            CurrentLevel = 0;
            PlayerPrefs.SetInt(PREFS_KEY, CurrentLevel);
            PlayerPrefs.Save();

            Debug.Log($"Reset progress");
        }
    }
}