namespace Assets.Source.Code_base
{
    public class Weapon : IReadOnlyWeapon
    {
        private readonly IInputAttacker _input;
        private readonly BulletPool _pool;

        public Weapon(IInputAttacker input, IWeaponStat weaponStat)
        {
            _input = input;
            _pool = new BulletPool(weaponStat);
        }

        public void Disable() 
        {

        }

        private void AttackLaser()
        {

        }

        private void AttackDefold()
        {

        }
    }
}