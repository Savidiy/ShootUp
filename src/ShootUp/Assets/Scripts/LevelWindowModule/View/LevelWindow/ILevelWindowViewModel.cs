using MvvmModule;
using UniRx;

namespace LevelWindowModule.View
{
    public interface ILevelWindowViewModel : IViewModel
    {
        IReadOnlyReactiveProperty<int> HeartCount { get; }
        IReadOnlyReactiveProperty<bool> IsWin { get; }
        bool IsComplete { get; }
        void SettingsClickFromView();
        void ClickRestartFromView();
        void ClickResetFromView();
    }
}