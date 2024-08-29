using UnityEngine;

namespace Assets.Source.Code_base
{
    public class BulletFactory : IFactory<Bullet>
    {
        private readonly WeaponConfig _weaponStat;
        private readonly AttackPoint _attackPoint;
        private readonly BulletPool _bulletPool;

        public BulletFactory(WeaponConfig weaponStat, AttackPoint attackPoint, BulletPool bulletPool)
        {
            _weaponStat = weaponStat;
            _attackPoint = attackPoint;
            _bulletPool = bulletPool;
        }

        public Bullet Create()
        {
            Bullet bullet = Object.Instantiate(
                _weaponStat.DefaultBulletPrefab,
                _attackPoint.Position,
                _attackPoint.Rotation);

            bullet.Init(_bulletPool);

            return bullet;
        }
    }
}
