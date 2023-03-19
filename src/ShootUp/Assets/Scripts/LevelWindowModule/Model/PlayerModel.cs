using UnityEngine;

namespace LevelWindowModule
{
    public class PlayerModel
    {
        private readonly PlayerHierarchy _playerHierarchy;

        public PlayerModel(PlayerHierarchy playerHierarchy)
        {
            _playerHierarchy = playerHierarchy;
        }

        public float PositionX { get; private set; }
        public bool IsSeeRight { get; private set; }
        public float GetWidth => _playerHierarchy.SpriteRenderer.bounds.size.x;
        public float GetHeight => _playerHierarchy.SpriteRenderer.bounds.size.y;

        public void SetPositionX(float x)
        {
            PositionX = x;

            Vector3 position = _playerHierarchy.transform.position;
            position.x = x;
            _playerHierarchy.transform.position = position;
        }

        public void SerIsSeeRight(bool isSeeRight)
        {
            IsSeeRight = isSeeRight;
        }

        public void SetPositionY(float y)
        {
            Vector3 position = _playerHierarchy.transform.position;
            position.y = y;
            _playerHierarchy.transform.position = position;
        }
    }
}