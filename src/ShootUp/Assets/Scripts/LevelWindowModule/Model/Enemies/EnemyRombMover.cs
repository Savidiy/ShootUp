using Savidiy.Utils;
using SettingsModule;
using UnityEngine;

namespace LevelWindowModule
{
    public class EnemyRombMover : ConcreteEnemyMover<RombMoveData>
    {
        private readonly BorderController _borderController;
        private readonly GameSettings _gameSettings;
        private readonly TickInvoker _tickInvoker;

        public EnemyRombMover(BorderController borderController, GameSettings gameSettings, TickInvoker tickInvoker)
        {
            _borderController = borderController;
            _gameSettings = gameSettings;
            _tickInvoker = tickInvoker;
        }

        protected override void ExecuteMove(Enemy enemy, RombMoveData data)
        {
            float deltaTime = _tickInvoker.DeltaTime;

            Vector3 position = enemy.Position;

            float deltaX = _gameSettings.SpeedX * deltaTime * (data.IsMoveRight ? 1 : -1);
            float deltaY = _gameSettings.SpeedY * deltaTime * (data.IsMoveUp ? 1 : -1);

            float newX = position.x + deltaX;

            float shiftX = enemy.Width / 2;
            float maxX = _borderController.MaxX - shiftX;
            float minX = _borderController.MinX + shiftX;

            if (newX >= maxX)
            {
                newX = maxX;
                data.IsMoveRight = false;
            }
            else if (newX <= minX)
            {
                newX = minX;
                data.IsMoveRight = true;
            }

            float newY = position.y + deltaY;

            float shiftY = enemy.Height / 2;
            float maxY = _borderController.MaxY - shiftY;
            float minY = _borderController.MinY + shiftY;

            if (newY >= maxY)
            {
                newY = maxY;
                data.IsMoveUp = false;
            }
            else if (newY <= minY)
            {
                newY = minY;
                data.IsMoveUp = true;
            }

            position.x = newX;
            position.y = newY;

            enemy.SetPosition(position);
        }
    }
}