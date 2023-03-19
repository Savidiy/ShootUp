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

        public bool HasProgress { get; }

        public StartWindowViewModel(IViewModelFactory viewModelFactory, ISettingsWindowPresenter settingsWindowPresenter,
            MainStateMachine mainStateMachine, ProgressProvider progressProvider) : base(viewModelFactory)
        {
            _progressProvider = progressProvider;
            _settingsWindowPresenter = settingsWindowPresenter;
            _mainStateMachine = mainStateMachine;
            HasProgress = progressProvider.CurrentLevel > 0;
        }

        public void StartClickFromView()
        {
            StartGame();
        }

        public void ContinueClickFromView()
        {
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
            _settingsWindowPresenter.ShowWindow();
        }
    }
}