using Savidiy.Utils;
using SettingsModule;
using UnityEngine;

namespace LevelWindowModule
{
    public sealed class EnemyMover
    {
        private readonly TickInvoker _tickInvoker;
        private readonly BorderController _borderController;
        private readonly CameraProvider _cameraProvider;
        private readonly EnemyHierarchy _enemyHierarchy;
        private bool _isMoveRight;
        private bool _isMoveUp;
        private GameSettings _gameSettings;

        public EnemyMover(GameSettings gameSettings, TickInvoker tickInvoker, BorderController borderController,
            CameraProvider cameraProvider, EnemiesHolder enemiesHolder)
        {
            _gameSettings = gameSettings;
            _tickInvoker = tickInvoker;
            _borderController = borderController;
            _cameraProvider = cameraProvider;

            _enemyHierarchy = Object.FindObjectOfType<EnemyHierarchy>();
            enemiesHolder.Add(new Enemy(_enemyHierarchy));
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
            float deltaTime = _tickInvoker.DeltaTime;

            Vector3 position = _enemyHierarchy.transform.position;

            float deltaX = _gameSettings.SpeedX * deltaTime * (_isMoveRight ? 1 : -1);
            float deltaY = _gameSettings.SpeedY * deltaTime * (_isMoveUp ? 1 : -1);

            float newX = position.x + deltaX;

            float shiftX = _enemyHierarchy.SpriteRenderer.bounds.size.x / 2;
            float maxX = _borderController.MaxX - shiftX;
            float minX = _borderController.MinX + shiftX;

            if (newX >= maxX)
            {
                newX = maxX;
                _isMoveRight = false;
            }
            else if (newX <= minX)
            {
                newX = minX;
                _isMoveRight = true;
            }

            float newY = position.y + deltaY;

            float shiftY = _enemyHierarchy.SpriteRenderer.bounds.size.y / 2;
            float maxY = _borderController.MaxY - shiftY;
            float minY = _borderController.MinY + shiftY;

            if (newY >= maxY)
            {
                newY = maxY;
                _isMoveUp = false;
            }
            else if (newY <= minY)
            {
                newY = minY;
                _isMoveUp = true;
            }

            position.x = newX;
            position.y = newY;

            _enemyHierarchy.transform.position = position;
        }
    }
}