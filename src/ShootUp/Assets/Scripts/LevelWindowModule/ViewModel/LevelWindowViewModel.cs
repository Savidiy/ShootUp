using AudioModule.Contracts;
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
        private readonly PlayerMover _playerMover;
        private readonly PlayerShooter _playerShooter;
        private readonly IAudioPlayer _audioPlayer;
        private readonly ReactiveProperty<bool> _isWin = new();
        private readonly ReactiveProperty<bool> _isMobileControl = new ();

        public IReadOnlyReactiveProperty<int> HeartCount { get; }
        public IReadOnlyReactiveProperty<bool> IsWin => _isWin;
        public bool IsComplete { get; private set; }
        public IReadOnlyReactiveProperty<bool> IsMobileControl => _isMobileControl;

        public LevelWindowViewModel(IViewModelFactory viewModelFactory, ISettingsWindowPresenter settingsWindowPresenter,
            PlayerHolder playerHolder, MainStateMachine mainStateMachine, EnemyAtLivesKiller enemyAtLivesKiller,
            ProgressProvider progressProvider, PlayerMover playerMover, PlayerShooter playerShooter, IAudioPlayer audioPlayer)
            : base(viewModelFactory)
        {
            _settingsWindowPresenter = settingsWindowPresenter;
            _mainStateMachine = mainStateMachine;
            _progressProvider = progressProvider;
            _playerMover = playerMover;
            _playerShooter = playerShooter;
            _audioPlayer = audioPlayer;
            HeartCount = playerHolder.PlayerModel.HeartCount;

            AddDisposable(enemyAtLivesKiller.AllEnemiesDead.Subscribe(OnAllEnemyDeadNext));
            AddDisposable(progressProvider.SelectedControlType.Subscribe(OnMobileControlChange));
        }

        private void OnMobileControlChange(EControlType controlType)
        {
            _isMobileControl.Value = controlType == EControlType.Mobile;
        }

        private void OnAllEnemyDeadNext(bool allEnemyDead)
        {
            IsComplete = _progressProvider.HasNextLevel;
            _isWin.Value = allEnemyDead;
        }

        public void SettingsClickFromView()
        {
            _audioPlayer.PlayClick();
            _settingsWindowPresenter.ShowWindow();
        }

        public void ClickRestartFromView()
        {
            _audioPlayer.PlayClick();
            _mainStateMachine.EnterToState<LevelPlayMainState>();
        }

        public void ClickResetFromView()
        {
            _audioPlayer.PlayClick();
            _progressProvider.ResetProgress();
            _mainStateMachine.EnterToState<StartMainState>();
        }

        public void PressShootFromView(bool isPressed) => _playerShooter.SetMobileShoot(isPressed);

        public void PressRightFromView(bool isPressed) => _playerMover.SetMobileRight(isPressed);

        public void PressLeftFromView(bool isPressed) => _playerMover.SetMobileLeft(isPressed);
    }
}