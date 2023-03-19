using Savidiy.Utils;
using SettingsModule;
using UnityEngine;

namespace LevelWindowModule
{
    public class PlayerShooter
    {
        private readonly TickInvoker _tickInvoker;
        private readonly PlayerHolder _playerHolder;
        private readonly BulletHolder _bulletHolder;
        private readonly GameSettings _gameSettings;
        private bool _isMobilePressed;

        public PlayerShooter(TickInvoker tickInvoker, PlayerHolder playerHolder, BulletHolder bulletHolder,
            GameSettings gameSettings)
        {
            _tickInvoker = tickInvoker;
            _playerHolder = playerHolder;
            _bulletHolder = bulletHolder;
            _gameSettings = gameSettings;
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
            bool canShoot = _playerHolder.PlayerModel.CanShoot;
            if (!canShoot)
                return;

            bool needShoot = GetNeedShoot();
            if (!needShoot)
                return;

            PlayerModel playerModel = _playerHolder.PlayerModel;
            _bulletHolder.AddPlayerBulletAt(playerModel.BulletStartPosition);
            playerModel.SetShootCooldown(_gameSettings.PlayerShootCooldown);
        }

        private bool GetNeedShoot()
        {
            bool needShoot = false;
            foreach (KeyCode keyCode in _gameSettings.ShootKeys)
                if (Input.GetKey(keyCode))
                    needShoot = true;

            if (_isMobilePressed)
                needShoot = true;
            
            return needShoot;
        }

        public void SetMobileShoot(bool isPressed) => _isMobilePressed = isPressed;
    }
}