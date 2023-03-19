using System;

namespace LevelWindowModule
{
    public abstract class ConcreteEnemyMover<T> : IConcreteEnemyMover
        where T : class, IEnemyMoveData
    {
        public bool IsMyType(Enemy enemy)
        {
            return enemy.EnemyMoveData.GetType() == typeof(T);
        }

        public void ExecuteMove(Enemy enemy)
        {
            if (enemy.EnemyMoveData is T data)
            {
                ExecuteMove(enemy, data);
            }
            else
            {
                throw new Exception("Incompatible type");
            }
        }

        protected abstract void ExecuteMove(Enemy enemy, T data);
    }
}