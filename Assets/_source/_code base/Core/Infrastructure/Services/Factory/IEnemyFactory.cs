namespace Assets.Source.Code_base
{
    public interface IEnemyFactory
    {
        Enemy Get(EnemyNames enemyName);
    }
}