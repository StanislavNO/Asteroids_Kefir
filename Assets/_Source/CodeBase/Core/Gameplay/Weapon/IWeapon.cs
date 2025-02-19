namespace Assets._Source.CodeBase.Core.Gameplay.Enemies
{
    public interface IWeapon : IReadOnlyWeapon 
    {
        bool TryAttackLaser();
        void AttackDefault();
    }
}