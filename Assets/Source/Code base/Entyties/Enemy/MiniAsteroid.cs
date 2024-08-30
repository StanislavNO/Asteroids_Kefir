namespace Assets.Source.Code_base
{
    public class MiniAsteroid : Asteroid
    {
        public override void Accept(IEnemyVisitor visitor) =>
            visitor.Visit(this);
    }
}