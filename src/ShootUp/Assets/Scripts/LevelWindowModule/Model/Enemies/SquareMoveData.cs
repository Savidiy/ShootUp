namespace LevelWindowModule
{
    public class SquareMoveData : IEnemyMoveData
    {
        public float MinX { get; }
        public float MaxX { get; }
        public float MinY { get; }
        public float MaxY { get; }
        public float Duration { get; }
        public bool IsClockwise { get; }
        public float Timer;
        
        public SquareMoveData(bool isClockwise, float minX, float maxX, float minY, float maxY, float timer, float duration)
        {
            MinX = minX;
            MaxX = maxX;
            MinY = minY;
            MaxY = maxY;
            Duration = duration;
            Timer = timer;
            IsClockwise = isClockwise;
        }
    }
}