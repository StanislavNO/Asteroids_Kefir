namespace Assets._Source.CodeBase.Meta.Services.Analytics
{
    public interface IEventWriter
    {
        void WriteGameOver();
        void WriteGameStart();
        void WriteAttackLaser();
    }
}