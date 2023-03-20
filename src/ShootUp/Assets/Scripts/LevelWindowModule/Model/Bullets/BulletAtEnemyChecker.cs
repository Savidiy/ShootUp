using System;
using AudioModule.Contracts;
using Savidiy.Utils;
using UnityEngine;

namespace LevelWindowModule
{
    public sealed class BulletAtEnemyChecker
    {
        private readonly TickInvoker _tickInvoker;
        private readonly BulletHolder _bulletHolder;
        private readonly EnemiesHolder _enemiesHolder;
        private readonly IAudioPlayer _audioPlayer;

        public BulletAtEnemyChecker(TickInvoker tickInvoker, BulletHolder bulletHolder, EnemiesHolder enemiesHolder,
            IAudioPlayer audioPlayer)
        {
            _tickInvoker = tickInvoker;
            _bulletHolder = bulletHolder;
            _enemiesHolder = enemiesHolder;
            _audioPlayer = audioPlayer;
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
                    int enemyLives = enemy.Lives - 1;
                    enemy.SetLives(enemyLives);
                    _audioPlayer.PlayOnce(GetSound(enemyLives, enemy.EnemyType));

                    return true;
                }
            }

            return false;
        }

        private static SoundId GetSound(int enemyLives, EEnemyType enemyType)
        {
            if (enemyLives == 0)
                return SoundId.EnemyDead;

            return enemyType switch
            {
                EEnemyType.Circle => SoundId.EnemyCircleHurt,
                EEnemyType.Romb => SoundId.EnemyRombHurt,
                EEnemyType.Triangle => SoundId.EnemyTriangleHurt,
                EEnemyType.Square => SoundId.EnemySquareHurt,
                _ => throw new ArgumentOutOfRangeException(nameof(enemyType), enemyType, null)
            };
        }
    }
}