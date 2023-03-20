﻿namespace LevelWindowModule
{
    public class RombMoveData : IEnemyMoveData
    {
        public bool IsMoveRight;
        public bool IsMoveUp;

        public RombMoveData(bool isMoveRight, bool isMoveUp)
        {
            IsMoveRight = isMoveRight;
            IsMoveUp = isMoveUp;
        }
    }
}