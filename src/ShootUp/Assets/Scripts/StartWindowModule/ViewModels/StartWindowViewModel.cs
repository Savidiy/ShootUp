using AudioModule.Contracts;
using MainModule;
using MvvmModule;
using Progress;
using SettingsWindowModule.Contracts;
using StartWindowModule.View;

namespace StartWindowModule
{
    public sealed class StartWindowViewModel : EmptyViewModel, IStartWindowViewModel
    {
        private readonly ISettingsWindowPresenter _settingsWindowPresenter;
        private readonly MainStateMachine _mainStateMachine;
        private readonly ProgressProvider _progressProvider;
        private readonly IAudioPlayer _audioPlayer;

        public bool HasProgress { get; }

        public StartWindowViewModel(IViewModelFactory viewModelFactory, ISettingsWindowPresenter settingsWindowPresenter,
            MainStateMachine mainStateMachine, ProgressProvider progressProvider, IAudioPlayer audioPlayer) : base(
            viewModelFactory)
        {
            _progressProvider = progressProvider;
            _audioPlayer = audioPlayer;
            _settingsWindowPresenter = settingsWindowPresenter;
            _mainStateMachine = mainStateMachine;
            HasProgress = progressProvider.CurrentLevel > 0;
        }

        public void StartClickFromView()
        {
            _audioPlayer.PlayClick();
            StartGame();
        }

        public void ContinueClickFromView()
        {
            _audioPlayer.PlayClick();
            StartGame();
        }

        private void StartGame()
        {
            if (_progressProvider.SelectedControlType.Value == EControlType.NotSelected)
                _mainStateMachine.EnterToState<SelectControlMainState>();
            else
                _mainStateMachine.EnterToState<LevelPlayMainState>();
        }

        public void SettingsClickFromView()
        {
            _audioPlayer.PlayClick();
            _settingsWindowPresenter.ShowWindow();
        }
    }
}