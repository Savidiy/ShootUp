using System.Collections.Generic;
using SettingsModule;
using UnityEngine;

namespace LevelWindowModule
{
    public class BulletHolder
    {
        private readonly GameSettings _gameSettings;
        private readonly GameObject _bulletRoot;
        private readonly List<Bullet> _bullets = new();

        public IReadOnlyList<Bullet> Bullets => _bullets;

        public BulletHolder(GameSettings gameSettings)
        {
            _gameSettings = gameSettings;
            _bulletRoot = new GameObject("BulletRoot");
        }

        public void AddPlayerBulletAt(Vector3 bulletStartPosition)
        {
            var bulletHierarchy = Object.Instantiate(_gameSettings.BulletPrefab, _bulletRoot.transform);

            var bullet = new Bullet(bulletHierarchy, bulletStartPosition);
            _bullets.Add(bullet);
        }

        public void RemoveBulletAt(int index)
        {
            Bullet bullet = _bullets[index];
            _bullets.RemoveAt(index);
            bullet.Destroy();
        }

        public void Clear()
        {
            for (var index = _bullets.Count - 1; index >= 0; index--)
            {
                RemoveBulletAt(index);
            }
        }
    }
}