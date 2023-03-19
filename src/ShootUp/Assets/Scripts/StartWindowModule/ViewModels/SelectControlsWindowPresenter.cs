using System;
using MvvmModule;
using StartWindowModule.Contracts;
using StartWindowModule.View;
using UiModule;
using UnityEngine;

namespace StartWindowModule
{
    public sealed class SelectControlsWindowPresenter : IDisposable, ISelectControlsWindowPresenter
    {
        private const string PREFAB_NAME = "SelectControls_window";
        private readonly WindowsRootProvider _windowsRootProvider;
        private readonly IViewFactory _viewFactory;
        private readonly IViewModelFactory _viewModelFactory;
        private readonly SelectControlsWindowView _view;

        public SelectControlsWindowPresenter(WindowsRootProvider windowsRootProvider, IViewFactory viewFactory,
            IViewModelFactory viewModelFactory)
        {
            _viewFactory = viewFactory;
            _viewModelFactory = viewModelFactory;
            _windowsRootProvider = windowsRootProvider;
            _view = CreateView();
            _view.SetActive(false);
        }

        public void ShowWindow()
        {
            var viewModel = _viewModelFactory.CreateEmptyViewModel<SelectControlsWindowViewModel>();
            _view.Initialize(viewModel);
            _view.SetActive(true);
        }

        public void HideWindow()
        {
            _view.ClearViewModel();
            _view.SetActive(false);
        }

        public void Dispose()
        {
            _view.Dispose();
        }

        private SelectControlsWindowView CreateView()
        {
            Transform root = _windowsRootProvider.GetWindowRoot();
            var view = _viewFactory.CreateView<SelectControlsWindowView, SelectControlsWindowHierarchy>(PREFAB_NAME, root);
            return view;
        }
    }
}