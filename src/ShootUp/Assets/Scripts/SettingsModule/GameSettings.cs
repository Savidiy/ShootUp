using System;
using System.Collections.Generic;
using LevelWindowModule;
using Savidiy.Utils;
using Sirenix.OdinInspector;
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
        public EnemyVector2 EnemySpeed;
        public KeyCode[] RightKeys;
        public KeyCode[] LeftKeys;
        public KeyCode[] ShootKeys;
        public AnimationCurve CircleSpeedX;
        public AnimationCurve CircleSpeedY;
        public float PlayerSpeed = 4;
        public float StartInvulDuration = 2;
        public float HitInvulDuration = 2;
        public float BlinkPeriod = 0.1f;
        public float PlayerShootCooldown = 0.2f;
        public BulletHierarchy BulletPrefab;
        public float BulletSpeed = 7f;
        public int StartHeartCount = 5;
        public EnemyInt EnemyLives;

        public Vector2 GetSpeed(EEnemyType enemyType)
        {
            if (EnemySpeed.TryGetValue(enemyType, out var speed))
            {
                return speed;
            }

            throw new Exception($"Can't find enemy '{enemyType}' speed");
        }
    }

    [Serializable]
    public class LevelData
    {
        public List<EnemySpawnData> Enemies;
    }

    [Serializable]
    public class EnemySpawnData
    {
        public EEnemyType EnemyType;

        [Range(0, 1)]
        public float PositionPercentX = 0.5f;

        [Range(0, 1)]
        public float PositionPercentY = 0.9f;

        [ShowIf(nameof(IsRombOrCircle))] public bool IsMoveRight;
        [ShowIf(nameof(IsRombOrCircle))] public bool IsMoveUp;
        [ShowIf(nameof(IsSquare))] public bool IsClockwise;
        [ShowIf(nameof(IsSquare))] public float StartTimer;
        [ShowIf(nameof(IsSquare))] public float Duration = 4;
        [Range(0, 1), ShowIf(nameof(IsSquare))] public float MinX;
        [Range(0, 1), ShowIf(nameof(IsSquare))] public float MaxX = 1;
        [Range(0, 1), ShowIf(nameof(IsSquare))] public float MinY;
        [Range(0, 1), ShowIf(nameof(IsCircleOrSquare))] public float MaxY = 1;

        private bool IsRombOrCircle() => EnemyType is EEnemyType.Romb or EEnemyType.Circle;
        private bool IsRomb() => EnemyType == EEnemyType.Romb;
        private bool IsSquare() => EnemyType == EEnemyType.Square;
        private bool IsTriangle() => EnemyType == EEnemyType.Triangle;
        private bool IsCircle() => EnemyType == EEnemyType.Circle;
        private bool IsCircleOrSquare() => EnemyType is EEnemyType.Circle or EEnemyType.Square;
    }

    [Serializable]
    public class EnemyInt : SerializedDictionary<EEnemyType, int>
    {
    }

    [Serializable]
    public class EnemyVector2 : SerializedDictionary<EEnemyType, Vector2>
    {
    }
}