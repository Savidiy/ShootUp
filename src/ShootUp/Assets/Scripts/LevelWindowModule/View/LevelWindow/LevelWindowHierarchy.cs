using UnityEngine;
using UnityEngine.UI;

namespace LevelWindowModule.View
{
    public sealed class LevelWindowHierarchy : MonoBehaviour
    {
        public Button SettingsButton;
        public HeartHierarchy HeartPrefab;
        public Transform HeartRoot;
        public GameObject RestartContainer;
        public Button RestartButton;
        public GameObject NextLevelContainer;
        public Button NextLevelButton;
        public GameObject CompleteContainer;
        public Button ResetButton;
    }
}