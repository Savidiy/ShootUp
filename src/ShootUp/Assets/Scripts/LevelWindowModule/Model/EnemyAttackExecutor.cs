using Savidiy.Utils;
using UnityEngine;

namespace LevelWindowModule
{
    public class EnemyAttackExecutor
    {
        private readonly TickInvoker _tickInvoker;
        private readonly EnemiesHolder _enemiesHolder;
        private readonly PlayerHolder _playerHolder;

        public EnemyAttackExecutor(TickInvoker tickInvoker, EnemiesHolder enemiesHolder, PlayerHolder playerHolder)
        {
            _tickInvoker = tickInvoker;
            _enemiesHolder = enemiesHolder;
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
            Collider2D playerCollider = _playerHolder.PlayerModel.Collider;
            foreach (IEnemy enemy in _enemiesHolder.Enemies)
            {
                if (_playerHolder.PlayerModel.IsInvulnerable)
                    return;

                Collider2D enemyCollider = enemy.Collider;
                ColliderDistance2D colliderDistance2D = Physics2D.Distance(playerCollider, enemyCollider);
                if (colliderDistance2D.isOverlapped)
                {
                    _playerHolder.PlayerModel.GetHit();
                }
            }
        }
    }
}