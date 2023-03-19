using MvvmModule;

namespace StartWindowModule.View
{
    public interface ISelectControlsWindowViewModel : IViewModel
    {
        void SelectMobileFromView();
        void SelectKeyboardFromView();
        string KeysText { get; }
        void SettingsClickFromView();
    }
}