using Savidiy.Utils;
using UnityEngine;

namespace LevelWindowModule
{
    public sealed class BulletAtEnemyChecker
    {
        private readonly TickInvoker _tickInvoker;
        private readonly BulletHolder _bulletHolder;
        private readonly EnemiesHolder _enemiesHolder;

        public BulletAtEnemyChecker(TickInvoker tickInvoker, BulletHolder bulletHolder, EnemiesHolder enemiesHolder)
        {
            _tickInvoker = tickInvoker;
            _bulletHolder = bulletHolder;
            _enemiesHolder = enemiesHolder;
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
            for (int index = _bulletHolder.Bullets.Count - 1; index >= 0; index--)
            {
                Bullet bullet = _bulletHolder.Bullets[index];
                
                if (BulletCollideEnemy(bullet))
                    _bulletHolder.RemoveBulletAt(index);
            }
        }

        private bool BulletCollideEnemy(Bullet bullet)
        {
            Collider2D bulletCollider = bullet.Collider;

            for (var index = 0; index < _enemiesHolder.Enemies.Count; index++)
            {
                Enemy enemy = _enemiesHolder.Enemies[index];
                Collider2D enemyCollider = enemy.Collider;
                
                ColliderDistance2D colliderDistance2D = Physics2D.Distance(bulletCollider, enemyCollider);

                if (colliderDistance2D.isOverlapped)
                {
                    enemy.SetLives(enemy.Lives - 1);
                    return true;
                }
            }

            return false;
        }
    }
}