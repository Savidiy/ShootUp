using System;
using AudioModule.Contracts;
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

        public PlayerHolder(BorderController borderController, GameSettings gameSettings, TickInvoker tickInvoker,
            IAudioPlayer audioPlayer)
        {
            _borderController = borderController;
            _gameSettings = gameSettings;
            _tickInvoker = tickInvoker;
            _tickInvoker.Updated += OnUpdated;
            var playerHierarchy = Object.FindObjectOfType<PlayerHierarchy>();
            PlayerModel = new PlayerModel(playerHierarchy, gameSettings, audioPlayer);
        }

        public void ResetPlayer()
        {
            const float startX = 0;
            PlayerModel.SetPositionX(startX);
            float startY = PlayerModel.GetHeight / 2 + _borderController.MinY;
            PlayerModel.SetPositionY(startY);
            PlayerModel.SetInvulnerableTimer(_gameSettings.StartInvulDuration);
            PlayerModel.SetHeartCount(_gameSettings.StartHeartCount);
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