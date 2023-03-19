using System;
using System.Collections.Generic;
using Savidiy.Utils;

namespace LevelWindowModule
{
    public sealed class EnemyMover
    {
        private readonly TickInvoker _tickInvoker;
        private readonly EnemiesHolder _enemiesHolder;
        private readonly List<IConcreteEnemyMover> _concreteEnemyMovers = new();

        public EnemyMover(TickInvoker tickInvoker,
            EnemiesHolder enemiesHolder, List<IConcreteEnemyMover> concreteEnemyMovers)
        {
            _enemiesHolder = enemiesHolder;
            _tickInvoker = tickInvoker;

            _concreteEnemyMovers.AddRange(concreteEnemyMovers);
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
            foreach (Enemy enemy in _enemiesHolder.Enemies)
            {
                Move(enemy);
            }
        }

        private void Move(Enemy enemy)
        {
            foreach (IConcreteEnemyMover concreteEnemyMover in _concreteEnemyMovers)
            {
                if (concreteEnemyMover.IsMyType(enemy))
                {
                    concreteEnemyMover.ExecuteMove(enemy);
                    return;
                }
            }

            throw new Exception($"Can't find concrete enemy mover for '{enemy.EnemyMoveData.GetType()}'");
        }
    }
}