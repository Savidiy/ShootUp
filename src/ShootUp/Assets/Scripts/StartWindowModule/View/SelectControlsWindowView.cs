using MvvmModule;

namespace StartWindowModule.View
{
    public sealed class SelectControlsWindowView : View<SelectControlsWindowHierarchy, ISelectControlsWindowViewModel>
    {
        public SelectControlsWindowView(SelectControlsWindowHierarchy hierarchy, IViewFactory viewFactory) : base(hierarchy, viewFactory)
        {
        }

        protected override void UpdateViewModel(ISelectControlsWindowViewModel viewModel)
        {
            Hierarchy.KeysLabel.text = viewModel.KeysText;
            
            BindClick(Hierarchy.KeyboardButton, OnKeyboardClick);
            BindClick(Hierarchy.MobileButton, OnMobileClick);
            BindClick(Hierarchy.SettingsButton, OnSettingsClick);
        }

        private void OnSettingsClick()
        {
            ViewModel.SettingsClickFromView();
        }

        private void OnMobileClick()
        {
            ViewModel.SelectMobileFromView();
        }

        private void OnKeyboardClick()
        {
            ViewModel.SelectKeyboardFromView();
        }
    }
}