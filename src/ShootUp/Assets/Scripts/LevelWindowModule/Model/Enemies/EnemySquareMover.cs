using Savidiy.Utils;
using SettingsModule;
using UnityEngine;

namespace LevelWindowModule
{
    public class EnemySquareMover : ConcreteEnemyMover<SquareMoveData>
    {
        private readonly BorderController _borderController;
        private readonly GameSettings _gameSettings;
        private readonly TickInvoker _tickInvoker;

        public EnemySquareMover(BorderController borderController, GameSettings gameSettings, TickInvoker tickInvoker)
        {
            _borderController = borderController;
            _gameSettings = gameSettings;
            _tickInvoker = tickInvoker;
        }

        protected override void ExecuteMove(Enemy enemy, SquareMoveData data)
        {
            float deltaTime = _tickInvoker.DeltaTime;
            if (data.IsClockwise)
            {
                data.Timer -= deltaTime;
                if (data.Timer < 0)
                    data.Timer += data.Duration;
            }
            else
            {
                data.Timer += deltaTime;
                if (data.Timer > data.Duration)
                    data.Timer -= data.Duration;
            }

            float progress = data.Timer / data.Duration * 4;


            Vector3 position = enemy.Position;
            float progressX;
            float progressY;

            if (progress < 1)
            {
                progressX = progress;
                progressY = 0;
            }
            else if (progress < 2)
            {
                progressX = 1;
                progressY = progress - 1;
            }
            else if (progress < 3)
            {
                progressX = 3 - progress;
                progressY = 1;
            }
            else
            {
                progressX = 0;
                progressY = 4 - progress;
            }


            float x = progressX * (data.MaxX - data.MinX) + data.MinX;
            position.x = x * (_borderController.MaxX - _borderController.MinX) + _borderController.MinX;
            float y = progressY * (data.MaxY - data.MinY) + data.MinY;
            position.y = y * (_borderController.MaxY - _borderController.MinY) + _borderController.MinY;

            enemy.SetPosition(position);
        }
    }
}