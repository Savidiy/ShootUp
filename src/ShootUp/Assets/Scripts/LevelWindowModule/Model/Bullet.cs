using SettingsModule;
using UnityEngine;

namespace LevelWindowModule
{
    public class Bullet
    {
        private readonly BulletHierarchy _bulletHierarchy;

        public Vector3 Position => _bulletHierarchy.transform.position;

        public Bullet(BulletHierarchy bulletHierarchy, Vector3 bulletStartPosition)
        {
            _bulletHierarchy = bulletHierarchy;
            SetPosition(bulletStartPosition);
        }

        public void SetPosition(Vector3 position)
        {
            _bulletHierarchy.transform.position = position;
        }

        public void Destroy()
        {
            Object.Destroy(_bulletHierarchy.gameObject);
        }
    }
}