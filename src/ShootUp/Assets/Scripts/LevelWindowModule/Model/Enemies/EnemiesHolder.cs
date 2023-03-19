using System.Collections.Generic;

namespace LevelWindowModule
{
    public class EnemiesHolder
    {
        private readonly List<Enemy> _enemies = new List<Enemy>();

        public IReadOnlyList<Enemy> Enemies => _enemies;

        public void Add(Enemy enemy)
        {
            _enemies.Add(enemy);
        }

        public void Clear()
        {
            foreach (Enemy enemy in _enemies)
            {
                enemy.Destroy();
            }
            
            _enemies.Clear();
        }

        public void RemoveAt(int index)
        {
            Enemy enemy = _enemies[index];
            enemy.Destroy();
            _enemies.RemoveAt(index);
        }
    }
}