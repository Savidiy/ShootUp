using System.Collections.Generic;

namespace LevelWindowModule
{
    public class EnemiesHolder
    {
        private readonly List<Enemy> _enemies = new List<Enemy>();

        public IReadOnlyList<IEnemy> Enemies => _enemies;

        public void Add(Enemy enemy)
        {
            _enemies.Add(enemy);
        }
    }
}