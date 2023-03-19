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
            PlayerModel playerModel = _playerHolder.PlayerModel;
            if (!playerModel.IsAlive)
                return;

            Collider2D playerCollider = playerModel.Collider;
            foreach (Enemy enemy in _enemiesHolder.Enemies)
            {
                if (playerModel.IsInvulnerable)
                    return;

                Collider2D enemyCollider = enemy.Collider;
                ColliderDistance2D colliderDistance2D = Physics2D.Distance(playerCollider, enemyCollider);
                if (colliderDistance2D.isOverlapped)
                {
                    playerModel.GetHit();
                }
            }
        }
    }
}