using MvvmModule;
using UniRx;

namespace LevelWindowModule.View
{
    public interface ILevelWindowViewModel : IViewModel
    {
        IReadOnlyReactiveProperty<int> HeartCount { get; }
        IReadOnlyReactiveProperty<bool> IsWin { get; }
        bool IsComplete { get; }
        IReadOnlyReactiveProperty<bool> IsMobileControl { get; }
        void SettingsClickFromView();
        void ClickRestartFromView();
        void ClickResetFromView();
        void PressShootFromView(bool isPressed);
        void PressRightFromView(bool isPressed);
        void PressLeftFromView(bool isPressed);
    }
}