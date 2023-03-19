using System;

namespace LevelWindowModule
{
    public interface IConcreteEnemyMover
    {
        bool IsMyType(Enemy enemy);
        void ExecuteMove(Enemy enemy);
    }
}