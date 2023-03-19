using MainModule;
using MvvmModule;
using Progress;
using Savidiy.Utils;
using SettingsModule;
using SettingsWindowModule.Contracts;
using StartWindowModule.View;

namespace StartWindowModule
{
    public sealed class SelectControlsWindowViewModel : EmptyViewModel, ISelectControlsWindowViewModel
    {
        private readonly ProgressProvider _progressProvider;

        private readonly MainStateMachine _mainStateMachine;
        private readonly ISettingsWindowPresenter _settingsWindowPresenter;

        public string KeysText { get; }

        public SelectControlsWindowViewModel(IViewModelFactory viewModelFactory, ProgressProvider progressProvider,
            GameSettings gameSettings, MainStateMachine mainStateMachine, ISettingsWindowPresenter settingsWindowPresenter)
            : base(viewModelFactory)
        {
            _progressProvider = progressProvider;
            _mainStateMachine = mainStateMachine;
            _settingsWindowPresenter = settingsWindowPresenter;

            KeysText = $"Move left:\n{gameSettings.LeftKeys.ToStringLine()}\n\n" +
                       $"Move right:\n{gameSettings.RightKeys.ToStringLine()}\n\n" +
                       $"Shoot:\n{gameSettings.ShootKeys.ToStringLine()}";
        }

        public void SelectMobileFromView()
        {
            _progressProvider.SetControls(EControlType.Mobile);
            _mainStateMachine.EnterToState<LevelPlayMainState>();
        }

        public void SelectKeyboardFromView()
        {
            _progressProvider.SetControls(EControlType.Keyboard);
            _mainStateMachine.EnterToState<LevelPlayMainState>();
        }

        public void SettingsClickFromView()
        {
            _settingsWindowPresenter.ShowWindow();
        }
    }
}