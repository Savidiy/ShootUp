using UnityEngine;

namespace LevelWindowModule
{
    public class Enemy
    {
        private readonly EnemyHierarchy _enemyHierarchy;

        public IEnemyMoveData EnemyMoveData { get; }
        public Collider2D Collider => _enemyHierarchy.Collider;
        public float Width => _enemyHierarchy.SpriteRenderer.bounds.size.x;
        public float Height => _enemyHierarchy.SpriteRenderer.bounds.size.y;
        public Vector3 Position => _enemyHierarchy.transform.position;
        public int Lives { get; private set; }
        public bool IsAlive => Lives > 0;
        public EEnemyType EnemyType { get; }

        public Enemy(EnemyHierarchy enemyHierarchy, IEnemyMoveData enemyMoveData, EEnemyType enemyType)
        {
            EnemyMoveData = enemyMoveData;
            EnemyType = enemyType;
            _enemyHierarchy = enemyHierarchy;
        }

        public void SetPosition(Vector3 position)
        {
            _enemyHierarchy.transform.position = position;
        }

        public void SetLives(int lives)
        {
            Lives = lives;
        }

        public void Destroy()
        {
            Object.Destroy(_enemyHierarchy.gameObject);
        }
    }
}