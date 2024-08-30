namespace Assets.Source.Code_base
{
    public interface IEnemyVisitor
    {
        void Visit(Asteroid enemy);
        void Visit(CharacterFollower characterFollower);
        void Visit(MiniAsteroid miniAsteroid);
    }
}
