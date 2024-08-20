namespace Assets.Source.Code_base
{
    public class BulletPool
    {
        private readonly IWeaponStat _weaponStat;

        public BulletPool(IWeaponStat weaponStat)
        {
            _weaponStat = weaponStat;
        }

    }
}