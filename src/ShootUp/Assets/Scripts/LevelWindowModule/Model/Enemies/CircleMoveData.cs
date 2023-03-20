namespace LevelWindowModule
{
    public class CircleMoveData : IEnemyMoveData
    {
        public float MaxY { get; }
        public bool IsMoveRight;
        public bool IsMoveUp;

        public CircleMoveData(bool isMoveRight, bool isMoveUp, float maxY)
        {
            MaxY = maxY;
            IsMoveRight = isMoveRight;
            IsMoveUp = isMoveUp;
        }
    }
}