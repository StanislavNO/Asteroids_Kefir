namespace Assets.Source.Code_base
{
    public interface IWeapon : IReadOnlyWeapon 
    {
        bool TryAttackLaser();
        void AttackDefold();
    }
}
