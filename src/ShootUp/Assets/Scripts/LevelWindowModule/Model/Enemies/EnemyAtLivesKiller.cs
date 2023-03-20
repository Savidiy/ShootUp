using AudioModule.Contracts;
using Progress;
using Savidiy.Utils;
using UniRx;

namespace LevelWindowModule
{
    public class EnemyAtLivesKiller
    {
        private readonly TickInvoker _tickInvoker;
        private readonly EnemiesHolder _enemiesHolder;
        private readonly ProgressProvider _progressProvider;
        private readonly IAudioPlayer _audioPlayer;
        private readonly ReactiveProperty<bool> _allEnemiesDead = new();

        public IReadOnlyReactiveProperty<bool> AllEnemiesDead => _allEnemiesDead;

        public EnemyAtLivesKiller(TickInvoker tickInvoker, EnemiesHolder enemiesHolder, ProgressProvider progressProvider,
            IAudioPlayer audioPlayer)
        {
            _tickInvoker = tickInvoker;
            _enemiesHolder = enemiesHolder;
            _progressProvider = progressProvider;
            _audioPlayer = audioPlayer;
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

            bool allEnemiesDead = _enemiesHolder.Enemies.Count == 0;

            if (!_allEnemiesDead.Value && allEnemiesDead)
            {
                _audioPlayer.PlayOnce(SoundId.WinLevel);
                _progressProvider.OpenNextLevel();
            }

            _allEnemiesDead.Value = allEnemiesDead;
        }
    }
}