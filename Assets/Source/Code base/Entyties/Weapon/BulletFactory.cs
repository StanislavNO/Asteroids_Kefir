using UnityEngine;

namespace Assets.Source.Code_base
{
    public class BulletFactory : IFactory<Bullet>
    {
        private readonly WeaponConfig _weaponStat;
        private readonly AttackPoint _attackPoint;
        private readonly PrefabsConfig _prefabsConfig;

        public BulletFactory(WeaponConfig weaponStat, AttackPoint attackPoint, PrefabsConfig prefabsConfig)
        {
            _weaponStat = weaponStat;
            _attackPoint = attackPoint;
            _prefabsConfig = prefabsConfig;
        }

        public Bullet Create()
        {
            Bullet bullet = Object.Instantiate(
                _prefabsConfig.DefaultBulletPrefab,
                _attackPoint.Position,
                _attackPoint.Rotation);

            return bullet;
        }
    }
}
