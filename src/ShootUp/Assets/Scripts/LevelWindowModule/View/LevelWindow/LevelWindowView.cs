using System.Collections.Generic;
using MvvmModule;
using UnityEngine;

namespace LevelWindowModule.View
{
    public sealed class LevelWindowView : View<LevelWindowHierarchy, ILevelWindowViewModel>
    {
        private readonly List<HeartHierarchy> _hearts = new();
        
        public LevelWindowView(LevelWindowHierarchy hierarchy, IViewFactory viewFactory) : base(hierarchy, viewFactory)
        {
        }

        protected override void UpdateViewModel(ILevelWindowViewModel viewModel)
        {
            BindClick(Hierarchy.SettingsButton, OnSettingsClick);
            BindClick(Hierarchy.RestartButton, OnRestartClick);
            BindClick(Hierarchy.NextLevelButton, OnRestartClick);
            BindClick(Hierarchy.ResetButton, OnResetClick);
            Bind(Hierarchy.LeftButton.IsPressed, OnLeftButtonClick);
            Bind(Hierarchy.RightButton.IsPressed, OnRightButtonClick);
            Bind(Hierarchy.ShootButton.IsPressed, OnShootButtonClick);
            
            Bind(viewModel.HeartCount, OnHeartCountChange);
            Bind(viewModel.IsWin, OnWinChange);
            Bind(viewModel.IsMobileControl, OnMobileControlChange);
        }

        private void OnShootButtonClick(bool isPressed) => ViewModel.PressShootFromView(isPressed);
        private void OnRightButtonClick(bool isPressed) => ViewModel.PressRightFromView(isPressed);
        private void OnLeftButtonClick(bool isPressed) => ViewModel.PressLeftFromView(isPressed);
        private void OnRestartClick() => ViewModel.ClickRestartFromView();
        private void OnResetClick() => ViewModel.ClickResetFromView();

        private void OnMobileControlChange(bool obj) => Hierarchy.MobileInputContainer.SetActive(obj);
        
        private void OnWinChange(bool isWin)
        {
            bool isComplete = ViewModel.IsComplete;
            Hierarchy.CompleteContainer.SetActive(isComplete);

            bool hasNextLevel = isWin && !isComplete;
            Hierarchy.NextLevelContainer.SetActive(hasNextLevel);
        }

        private void OnHeartCountChange(int heartCount)
        {
            Hierarchy.RestartContainer.SetActive(heartCount == 0);
            
            for (int i = _hearts.Count - 1; i >= heartCount; i--)
            {
                HeartHierarchy heartHierarchy = _hearts[i];
                Object.Destroy(heartHierarchy.gameObject);
                _hearts.RemoveAt(i);
            }

            for (int i = _hearts.Count; i < heartCount; i++)
            {
                HeartHierarchy heartHierarchy = Object.Instantiate(Hierarchy.HeartPrefab, Hierarchy.HeartRoot);
                _hearts.Add(heartHierarchy);
            }
        }

        private void OnSettingsClick()
        {
            ViewModel.SettingsClickFromView();
        }
    }
}