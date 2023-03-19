using LevelWindowModule.View;
using MainModule;
using MvvmModule;
using Progress;
using SettingsWindowModule.Contracts;
using UniRx;

namespace LevelWindowModule
{
    public sealed class LevelWindowViewModel : EmptyViewModel, ILevelWindowViewModel
    {
        private readonly ISettingsWindowPresenter _settingsWindowPresenter;
        private readonly MainStateMachine _mainStateMachine;
        private readonly ProgressProvider _progressProvider;
        private readonly ReactiveProperty<bool> _isWin = new();

        public IReadOnlyReactiveProperty<int> HeartCount { get; }
        public IReadOnlyReactiveProperty<bool> IsWin => _isWin;
        public bool IsComplete { get; private set; }

        public LevelWindowViewModel(IViewModelFactory viewModelFactory, ISettingsWindowPresenter settingsWindowPresenter,
            PlayerHolder playerHolder, MainStateMachine mainStateMachine, EnemyAtLivesKiller enemyAtLivesKiller,
            ProgressProvider progressProvider)
            : base(viewModelFactory)
        {
            _settingsWindowPresenter = settingsWindowPresenter;
            _mainStateMachine = mainStateMachine;
            _progressProvider = progressProvider;
            HeartCount = playerHolder.PlayerModel.HeartCount;

            AddDisposable(enemyAtLivesKiller.AllEnemiesDead.Subscribe(OnAllEnemyDeadNext));
        }

        private void OnAllEnemyDeadNext(bool allEnemyDead)
        {
            IsComplete = _progressProvider.HasNextLevel;
            _isWin.Value = allEnemyDead;
        }

        public void SettingsClickFromView()
        {
            _settingsWindowPresenter.ShowWindow();
        }

        public void ClickRestartFromView()
        {
            _mainStateMachine.EnterToState<LevelPlayMainState>();
        }

        public void ClickResetFromView()
        {
            _progressProvider.ResetProgress();
            _mainStateMachine.EnterToState<StartMainState>();
        }
    }
}