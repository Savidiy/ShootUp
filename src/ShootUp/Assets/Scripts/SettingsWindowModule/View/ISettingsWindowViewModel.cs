using MvvmModule;

namespace SettingsWindowModule.View
{
    public interface ISettingsWindowViewModel : IViewModel
    {
        void BackClickFromView();
        void ResetClickFromView();
    }
}