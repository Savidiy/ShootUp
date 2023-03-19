using System;
using System.Collections.Generic;
using Savidiy.Utils;
using UnityEngine;

namespace SettingsModule
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "GameSettings", order = 0)]
    public class GameSettings : AutoSaveScriptableObject
    {
        public List<LevelData> Levels;
        public float LeftBorderShift;
        public float BottomBorderShift;
        public float RightBorderShift;
        public float BorderScaleFactor = 5;
        public float UpLimitPixelShift = 50;
        public float SpeedX = 20f;
        public float SpeedY = 20f;
    }

    [Serializable]
    public class LevelData
    {
        public int Value = 1;
    }
}