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
            Bind(viewModel.HeartCount, OnHeartCountChange);
        }

        private void OnRestartClick()
        {
            ViewModel.ClickRestartFromView();
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