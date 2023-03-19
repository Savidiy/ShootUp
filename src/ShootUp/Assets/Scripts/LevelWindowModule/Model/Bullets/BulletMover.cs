using Savidiy.Utils;
using SettingsModule;
using UnityEngine;

namespace LevelWindowModule
{
    public class BulletMover
    {
        private readonly TickInvoker _tickInvoker;
        private readonly BulletHolder _bulletHolder;
        private readonly GameSettings _gameSettings;

        public BulletMover(TickInvoker tickInvoker, BulletHolder bulletHolder, GameSettings gameSettings)
        {
            _tickInvoker = tickInvoker;
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
            float deltaY = _gameSettings.BulletSpeed * _tickInvoker.DeltaTime; 
            
            foreach (Bullet bullet in _bulletHolder.Bullets)
            {
                Vector3 position = bullet.Position;
                position.y += deltaY;
                bullet.SetPosition(position);
            }
        }
    }
}