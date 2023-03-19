using System;
using System.Collections.Generic;
using LevelWindowModule.Contracts;
using Sirenix.OdinInspector;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace SettingsModule
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "GameSettings", order = 0)]
    public class GameSettings : SerializedScriptableObject
    {
        public List<LevelData> Levels;

        private void OnValidate()
        {
#if UNITY_EDITOR
            AssetDatabase.SaveAssetIfDirty(this);
#endif
        }
    }

    [Serializable]
    public class LevelData
    {
    }
}