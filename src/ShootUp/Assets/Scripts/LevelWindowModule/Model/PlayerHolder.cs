using UnityEngine;

namespace LevelWindowModule
{
    public sealed class PlayerHolder
    {
        private readonly BorderController _borderController;
        public PlayerModel PlayerModel { get; }

        public PlayerHolder(BorderController borderController)
        {
            _borderController = borderController;
            var playerHierarchy = Object.FindObjectOfType<PlayerHierarchy>();
            PlayerModel = new PlayerModel(playerHierarchy);
        }
        
        public void ResetPlayer()
        {
            const float startX = 0;
            PlayerModel.SetPositionX(startX);
            float startY = PlayerModel.GetHeight / 2 + _borderController.MinY;
            PlayerModel.SetPositionY(startY);
        }
    }
}