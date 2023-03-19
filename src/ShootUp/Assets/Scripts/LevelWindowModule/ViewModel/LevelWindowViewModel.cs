using LevelWindowModule.View;
using MainModule;
using MvvmModule;
using SettingsWindowModule.Contracts;
using UniRx;

namespace LevelWindowModule
{
    public sealed class LevelWindowViewModel : EmptyViewModel, ILevelWindowViewModel
    {
        private readonly ISettingsWindowPresenter _settingsWindowPresenter;
        private readonly MainStateMachine _mainStateMachine;
        public IReadOnlyReactiveProperty<int> HeartCount { get; }

        public LevelWindowViewModel(IViewModelFactory viewModelFactory, ISettingsWindowPresenter settingsWindowPresenter,
            PlayerHolder playerHolder, MainStateMachine mainStateMachine) : base(viewModelFactory)
        {
            _settingsWindowPresenter = settingsWindowPresenter;
            _mainStateMachine = mainStateMachine;
            HeartCount = playerHolder.PlayerModel.HeartCount;
        }

        public void SettingsClickFromView()
        {
            _settingsWindowPresenter.ShowWindow();
        }

        public void ClickRestartFromView()
        {
            _mainStateMachine.EnterToState<LevelPlayMainState>();
        }
    }
}