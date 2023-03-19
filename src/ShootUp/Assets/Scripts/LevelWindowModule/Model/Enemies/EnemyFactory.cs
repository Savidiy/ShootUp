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

        public EnemyFactory(EnemyPrefabProvider enemyPrefabProvider, BorderController borderController)
        {
            _enemyPrefabProvider = enemyPrefabProvider;
            _borderController = borderController;
        }

        public Enemy CreateEnemy(EnemySpawnData enemySpawnData)
        {
            EEnemyType enemyType = enemySpawnData.EnemyType;
            EnemyHierarchy hierarchy = InstantiateHierarchy(enemyType);

            IEnemyMoveData asd = CreateMoveData(enemySpawnData);
            var enemy = new Enemy(hierarchy, asd);

            float maxX = _borderController.MaxX;
            float maxY = _borderController.MaxY;
            float minX = _borderController.MinX;
            float minY = _borderController.MinY;
            float x = (maxX - minX) * enemySpawnData.PositionPercentX + minX;
            float y = (maxY - minY) * enemySpawnData.PositionPercentY + minY;
            var position = new Vector3(x, y, 0);

            enemy.SetPosition(position);
            return enemy;
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
                case EEnemyType.Romb:
                case EEnemyType.Triangle:
                case EEnemyType.Square:
                    return new RombMoveData();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}