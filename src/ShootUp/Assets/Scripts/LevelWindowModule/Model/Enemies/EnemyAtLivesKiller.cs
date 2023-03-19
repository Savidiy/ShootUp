using Savidiy.Utils;
using UniRx;

namespace LevelWindowModule
{
    public class EnemyAtLivesKiller
    {
        private readonly TickInvoker _tickInvoker;
        private readonly EnemiesHolder _enemiesHolder;
        private readonly ReactiveProperty<bool> _allEnemyDied = new();

        public IReadOnlyReactiveProperty<bool> AllEnemyDied => _allEnemyDied;

        public EnemyAtLivesKiller(TickInvoker tickInvoker, EnemiesHolder enemiesHolder)
        {
            _tickInvoker = tickInvoker;
            _enemiesHolder = enemiesHolder;
        }

        public void Activate()
        {
            _tickInvoker.Updated -= OnUpdated;
            _tickInvoker.Updated += OnUpdated;
        }

        public void Deactivate()
        {
            _tickInvoker.Updated -= OnUpdated;
        }

        private void OnUpdated()
        {
            for (var index = _enemiesHolder.Enemies.Count - 1; index >= 0; index--)
            {
                Enemy enemy = _enemiesHolder.Enemies[index];
                if (!enemy.IsAlive)
                    _enemiesHolder.RemoveAt(index);
            }

            _allEnemyDied.Value = _enemiesHolder.Enemies.Count == 0;
        }
    }
}