namespace Assets.Source.Code_base
{
    public interface IBulletFactory
    {
        void Init(AttackPoint attackPoint);
        Bullet Get();
    }
}