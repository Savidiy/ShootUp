using System;
using MainModule;
using MvvmModule;
using Progress;
using SettingsWindowModule.View;

namespace SettingsWindowModule
{
    public sealed class SettingsWindowViewModel : EmptyViewModel, ISettingsWindowViewModel
    {
        private readonly ProgressProvider _progressProvider;
        private readonly MainStateMachine _mainStateMachine;
        private readonly Type _returnStateType;

        public event Action NeedClose;

        public SettingsWindowViewModel(IViewModelFactory viewModelFactory, ProgressProvider progressProvider,
            MainStateMachine mainStateMachine)
            : base(viewModelFactory)
        {
            _progressProvider = progressProvider;
            _mainStateMachine = mainStateMachine;
        }

        public void BackClickFromView()
        {
            NeedClose?.Invoke();
        }

        public void ResetClickFromView()
        {
            _progressProvider.ResetProgress();
            NeedClose?.Invoke();
            _mainStateMachine.EnterToState<StartMainState>();
        }

        public void SelectMobileFromView()
        {
            _progressProvider.SetControls(EControlType.Mobile);
        }

        public void SelectKeyboardFromView()
        {
            _progressProvider.SetControls(EControlType.Keyboard);
        }
    }
}