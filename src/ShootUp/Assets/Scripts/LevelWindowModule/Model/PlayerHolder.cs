using System;
using Savidiy.Utils;
using SettingsModule;
using UnityEngine;
using Object = UnityEngine.Object;

namespace LevelWindowModule
{
    public sealed class PlayerHolder : IDisposable
    {
        private readonly BorderController _borderController;
        private readonly GameSettings _gameSettings;
        private readonly TickInvoker _tickInvoker;
        public PlayerModel PlayerModel { get; }

        public PlayerHolder(BorderController borderController, GameSettings gameSettings, TickInvoker tickInvoker)
        {
            _borderController = borderController;
            _gameSettings = gameSettings;
            _tickInvoker = tickInvoker;
            _tickInvoker.Updated += OnUpdated;
            var playerHierarchy = Object.FindObjectOfType<PlayerHierarchy>();
            PlayerModel = new PlayerModel(playerHierarchy, gameSettings);
        }
        
        public void ResetPlayer()
        {
            const float startX = 0;
            PlayerModel.SetPositionX(startX);
            float startY = PlayerModel.GetHeight / 2 + _borderController.MinY;
            PlayerModel.SetPositionY(startY);
            PlayerModel.SetInvulnerableTimer(_gameSettings.StartInvulDuration);
        }

        public void GetHit()
        {
            Debug.Log("Get Hit");
            PlayerModel.SetInvulnerableTimer(_gameSettings.HitInvulDuration);
        }

        public void Dispose()
        {
            _tickInvoker.Updated -= OnUpdated;
        }

        private void OnUpdated()
        {
            PlayerModel.Update(_tickInvoker.DeltaTime);
        }
    }
}