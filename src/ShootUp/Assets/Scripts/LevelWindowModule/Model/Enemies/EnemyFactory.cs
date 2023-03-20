using System;
using SettingsModule;
using UnityEngine;
using Object = UnityEngine.Object;

namespace LevelWindowModule
{
    public class EnemyFactory
    {
        private readonly EnemyPrefabProvider _enemyPrefabProvider;
        private readonly BorderController _borderController;
        private readonly GameSettings _gameSettings;

        public EnemyFactory(EnemyPrefabProvider enemyPrefabProvider, BorderController borderController, GameSettings gameSettings)
        {
            _enemyPrefabProvider = enemyPrefabProvider;
            _borderController = borderController;
            _gameSettings = gameSettings;
        }

        public Enemy CreateEnemy(EnemySpawnData enemySpawnData)
        {
            EEnemyType enemyType = enemySpawnData.EnemyType;
            EnemyHierarchy hierarchy = InstantiateHierarchy(enemyType);

            IEnemyMoveData enemyMoveData = CreateMoveData(enemySpawnData);
            var enemy = new Enemy(hierarchy, enemyMoveData, enemyType);

            float maxX = _borderController.MaxX;
            float maxY = _borderController.MaxY;
            float minX = _borderController.MinX;
            float minY = _borderController.MinY;
            float x = (maxX - minX) * enemySpawnData.PositionPercentX + minX;
            float y = (maxY - minY) * enemySpawnData.PositionPercentY + minY;
            var position = new Vector3(x, y, 0);

            enemy.SetPosition(position);

            int lives = GetStartEnemyLives(enemyType);
            enemy.SetLives(lives);
            return enemy;
        }

        private int GetStartEnemyLives(EEnemyType enemyType)
        {
            if (_gameSettings.EnemyLives.TryGetValue(enemyType, out int lives))
            {
                return lives;
            }

            throw new Exception($"Can't find start lives for '{enemyType}'");
        }

        private EnemyHierarchy InstantiateHierarchy(EEnemyType enemyType)
        {
            EnemyHierarchy enemyPrefab = _enemyPrefabProvider.GetPrefab(enemyType);
            EnemyHierarchy hierarchy = Object.Instantiate(enemyPrefab);
            return hierarchy;
        }

        private IEnemyMoveData CreateMoveData(EnemySpawnData enemySpawnData)
        {
            switch (enemySpawnData.EnemyType)
            {
                case EEnemyType.Circle:
                    return new CircleMoveData(enemySpawnData.IsMoveRight, enemySpawnData.IsMoveUp, enemySpawnData.MaxY);
                case EEnemyType.Triangle:
                case EEnemyType.Square:
                case EEnemyType.Romb:
                    return new RombMoveData(enemySpawnData.IsMoveRight, enemySpawnData.IsMoveUp);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}