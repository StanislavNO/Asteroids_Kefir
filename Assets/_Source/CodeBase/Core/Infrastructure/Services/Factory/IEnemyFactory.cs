using Assets._Source.CodeBase.Core.Gameplay.Enemies;

namespace Assets._Source.CodeBase.Core.Infrastructure.Services.Factory
{
    public interface IEnemyFactory
    {
        Enemy Get(EnemyNames enemyName);
    }
}