using UnityEngine;

namespace LevelWindowModule
{
    public class Enemy : IEnemy
    {
        private readonly EnemyHierarchy _enemyHierarchy;

        public Collider2D Collider => _enemyHierarchy.Collider;

        public Enemy(EnemyHierarchy enemyHierarchy)
        {
            _enemyHierarchy = enemyHierarchy;
        }
    }
}