using System;
using MvvmModule;
using SettingsWindowModule.View;

namespace SettingsWindowModule
{
    public sealed class SettingsWindowViewModel : EmptyViewModel, ISettingsWindowViewModel
    {
        private readonly Type _returnStateType;

        public event Action NeedClose;
        
        public SettingsWindowViewModel(IViewModelFactory viewModelFactory) : base(
            viewModelFactory)
        {
        }

        public void BackClickFromView()
        {
            NeedClose?.Invoke();
        }

        public void ResetClickFromView()
        {
        }
    }
}