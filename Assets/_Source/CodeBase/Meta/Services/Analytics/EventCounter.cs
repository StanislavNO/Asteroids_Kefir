namespace Assets._Source.CodeBase.Meta.Services.Analytics
{
    internal class EventCounter : IStatisticsWriter
    {
        public int DefaultAttack { get; private set; } = 0;
        public int LaserAttack { get; private set; } = 0;
        public int DeadAsteroids { get; private set; } = 0;
        public int DeadUfo { get; private set; } = 0;

        public void CountAsteroidDeath() =>
            DeadAsteroids++;

        public void CountUfoDeath() =>
            DeadUfo++;

        public void WriteLaserAttack() =>
            LaserAttack++;

        public void CountDefaultAttack() =>
            DefaultAttack++;
    }
}