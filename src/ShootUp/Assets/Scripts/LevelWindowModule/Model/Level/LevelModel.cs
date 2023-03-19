using System.Collections.Generic;
using MvvmModule;
using Savidiy.Utils;
using SettingsModule;

namespace LevelWindowModule
{
    public sealed class LevelModel : DisposableCollector
    {
        private readonly LevelData _levelData;
        private readonly TickInvoker _tickInvoker;
        private readonly EnemiesHolder _enemiesHolder;
        private readonly EnemyFactory _enemyFactory;

        public LevelModel(LevelData levelData, TickInvoker tickInvoker, EnemiesHolder enemiesHolder, EnemyFactory enemyFactory)
        {
            _levelData = levelData;
            _tickInvoker = tickInvoker;
            _enemiesHolder = enemiesHolder;
            _enemyFactory = enemyFactory;

            CreateEnemies(_levelData.Enemies);
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