using MvvmModule;
using UniRx;

namespace LevelWindowModule.View
{
    public interface ILevelWindowViewModel : IViewModel
    {
        void SettingsClickFromView();
        IReadOnlyReactiveProperty<int> HeartCount { get; }
    }
}