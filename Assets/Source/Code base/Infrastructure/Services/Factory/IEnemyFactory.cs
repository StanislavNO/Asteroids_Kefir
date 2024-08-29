namespace Assets.Source.Code_base
{
    public interface IEnemyFactory
    {
        Enemy Create(EnemyNames enemyName);
    }
}