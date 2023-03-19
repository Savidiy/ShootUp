using Savidiy.Utils;
using SettingsModule;

namespace LevelWindowModule
{
    public sealed class LevelModelFactory
    {
        private readonly TickInvoker _tickInvoker;
        private readonly EnemiesHolder _enemiesHolder;
        private readonly EnemyFactory _enemyFactory;

        public LevelModelFactory(TickInvoker tickInvoker, EnemiesHolder enemiesHolder, EnemyFactory enemyFactory)
        {
            _tickInvoker = tickInvoker;
            _enemiesHolder = enemiesHolder;
            _enemyFactory = enemyFactory;
        }

        public LevelModel Create(LevelData levelData)
        {
            var levelModel = new LevelModel(levelData, _enemiesHolder, _enemyFactory);
            return levelModel;
        }
    }
}