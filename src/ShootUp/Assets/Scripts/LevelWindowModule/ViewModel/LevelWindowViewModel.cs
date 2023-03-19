using LevelWindowModule.View;
using MvvmModule;
using SettingsWindowModule.Contracts;
using UniRx;

namespace LevelWindowModule
{
    public sealed class LevelWindowViewModel : EmptyViewModel, ILevelWindowViewModel
    {
        private readonly ISettingsWindowPresenter _settingsWindowPresenter;
        public IReadOnlyReactiveProperty<int> HeartCount { get; }

        public LevelWindowViewModel(IViewModelFactory viewModelFactory, ISettingsWindowPresenter settingsWindowPresenter, PlayerHolder playerHolder) : base(viewModelFactory)
        {
            _settingsWindowPresenter = settingsWindowPresenter;
            HeartCount = playerHolder.PlayerModel.HeartCount;
        }

        public void SettingsClickFromView()
        {
            _settingsWindowPresenter.ShowWindow();
        }
    }
}