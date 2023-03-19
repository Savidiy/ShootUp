using System.Collections.Generic;
using MvvmModule;
using SettingsModule;

namespace LevelWindowModule
{
    public sealed class LevelModel : DisposableCollector
    {
        private readonly EnemiesHolder _enemiesHolder;
        private readonly EnemyFactory _enemyFactory;

        public LevelModel(LevelData levelData, EnemiesHolder enemiesHolder, EnemyFactory enemyFactory)
        {
            _enemiesHolder = enemiesHolder;
            _enemyFactory = enemyFactory;

            CreateEnemies(levelData.Enemies);
        }

        public static LevelModel CreateEmpty()
        {
            return new LevelModel();
        }

        private LevelModel()
        {
        }

        private void CreateEnemies(List<EnemySpawnData> enemySpawnDatas)
        {
            foreach (EnemySpawnData enemySpawnData in enemySpawnDatas)
            {
                Enemy enemy = _enemyFactory.CreateEnemy(enemySpawnData);
                _enemiesHolder.Add(enemy);
            }
        }
    }
}