using Savidiy.Utils;
using SettingsModule;
using UnityEngine;

namespace LevelWindowModule
{
    public sealed class PlayerMover
    {
        private readonly TickInvoker _tickInvoker;
        private readonly GameSettings _gameSettings;
        private readonly BorderController _borderController;
        private readonly PlayerHolder _playerHolder;
        private bool _isMobileRight;
        private bool _isMobileLeft;

        public PlayerMover(GameSettings gameSettings, TickInvoker tickInvoker, BorderController borderController,
            PlayerHolder playerHolder)
        {
            _gameSettings = gameSettings;
            _tickInvoker = tickInvoker;
            _borderController = borderController;
            _playerHolder = playerHolder;
        }

        public void Activate()
        {
            _tickInvoker.Updated -= OnUpdated;
            _tickInvoker.Updated += OnUpdated;
        }

        public void Deactivate()
        {
            _tickInvoker.Updated -= OnUpdated;
        }

        private void OnUpdated()
        {
            PlayerModel playerModel = _playerHolder.PlayerModel;
            if (!playerModel.IsAlive)
                return;

            float halfPlayerWidth = playerModel.GetWidth / 2;
            float positionX = playerModel.PositionX;

            float deltaX = GetDeltaX();

            positionX += deltaX;
            float maxX = _borderController.MaxX - halfPlayerWidth;
            float minX = _borderController.MinX + halfPlayerWidth;

            if (positionX < minX)
                positionX = minX;
            else if (positionX > maxX)
                positionX = maxX;

            playerModel.SetPositionX(positionX);
        }

        private float GetDeltaX()
        {
            var moveToRight = false;
            var moveToLeft = false;
            float deltaX = _tickInvoker.DeltaTime * _gameSettings.PlayerSpeed;

            foreach (KeyCode keyCode in _gameSettings.RightKeys)
                if (Input.GetKey(keyCode))
                    moveToRight = true;

            foreach (KeyCode keyCode in _gameSettings.LeftKeys)
                if (Input.GetKey(keyCode))
                    moveToLeft = true;

            if (_isMobileLeft)
                moveToLeft = true;

            if (_isMobileRight)
                moveToRight = true;

            if (!moveToLeft && moveToRight)
                return deltaX;

            if (moveToLeft && !moveToRight)
                return -deltaX;

            return 0;
        }

        public void SetMobileRight(bool isMobileRight) => _isMobileRight = isMobileRight;
        public void SetMobileLeft(bool isMobileLeft) => _isMobileLeft = isMobileLeft;
    }
}