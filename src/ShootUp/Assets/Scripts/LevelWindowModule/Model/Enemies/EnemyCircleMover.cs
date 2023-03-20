using Savidiy.Utils;
using SettingsModule;
using UnityEngine;

namespace LevelWindowModule
{
    public class EnemyCircleMover : ConcreteEnemyMover<CircleMoveData>
    {
        private readonly BorderController _borderController;
        private readonly GameSettings _gameSettings;
        private readonly TickInvoker _tickInvoker;

        public EnemyCircleMover(BorderController borderController, GameSettings gameSettings, TickInvoker tickInvoker)
        {
            _borderController = borderController;
            _gameSettings = gameSettings;
            _tickInvoker = tickInvoker;
        }

        protected override void ExecuteMove(Enemy enemy, CircleMoveData data)
        {
            float deltaTime = _tickInvoker.DeltaTime;

            Vector3 position = enemy.Position;

            Vector2 speed = _gameSettings.GetSpeed(enemy.EnemyType);

            float percentY = (position.y - _borderController.MinY) / (_borderController.MaxY - _borderController.MinY) / data.MaxY;
            float speedX = _gameSettings.CircleSpeedX.Evaluate(percentY) * speed.x;
            float deltaX = speedX * deltaTime * (data.IsMoveRight ? 1 : -1);

            Debug.Log($"percentY={percentY:f2}   speedX={speedX:f2}");
            
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

            float speedY = _gameSettings.CircleSpeedY.Evaluate(percentY) * speed.y;
            float deltaY = speedY * deltaTime * (data.IsMoveUp ? 1 : -1);
            float newY = position.y + deltaY;

            float shiftY = enemy.Height / 2;
            float maxY = (_borderController.MaxY - _borderController.MinY) * data.MaxY + _borderController.MinY;
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