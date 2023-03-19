using Savidiy.Utils;
using SettingsModule;
using UnityEngine;

namespace LevelWindowModule
{
    public class BulletAtBorderKiller
    {
        private readonly TickInvoker _tickInvoker;
        private readonly BulletHolder _bulletHolder;
        private readonly BorderController _borderController;

        public BulletAtBorderKiller(TickInvoker tickInvoker, BulletHolder bulletHolder, BorderController borderController)
        {
            _tickInvoker = tickInvoker;
            _bulletHolder = bulletHolder;
            _borderController = borderController;
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
            float maxY = _borderController.MaxY;
            for (var index = _bulletHolder.Bullets.Count - 1; index >= 0; index--)
            {
                Bullet bullet = _bulletHolder.Bullets[index];
                Vector3 position = bullet.Position;
                if (position.y > maxY)
                    _bulletHolder.RemoveBulletAt(index);
            }
        }
    }
}