namespace Assets._Source.CodeBase.Meta.Services.Analytics
{
    public interface IStatisticsWriter
    {
        void CountAsteroidDeath();
        void CountUfoDeath();
        void CountDefaultAttack();
    }
}