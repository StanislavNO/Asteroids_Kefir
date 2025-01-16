using Assets._Source.CodeBase.Core.Gameplay.Enemies;

namespace Assets._Source.CodeBase.Core.Infrastructure.Services.Factory
{
    public interface IBulletFactory
    {
        void Init(AttackPoint attackPoint);
        Bullet Get();
    }
}