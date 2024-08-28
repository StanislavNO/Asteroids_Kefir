using System.Collections.Generic;
using UnityEngine;

namespace Assets.Source.Code_base
{
    public class BulletPool : IPool
    {
        private readonly Queue<Bullet> _bullets;
        private readonly BulletFactory _bulletFactory;
        private readonly AttackPoint _attackPoint;

        public BulletPool(WeaponConfig weaponStat, AttackPoint attackPoint)
        {
            _bulletFactory = new(weaponStat, attackPoint, this);
            _attackPoint = attackPoint;
            _bullets = new Queue<Bullet>();
        }

        public Bullet Get()
        {
            if (_bullets.Count == 0)
                return _bulletFactory.Create();

            Bullet bullet = _bullets.Dequeue();
            bullet.gameObject.SetActive(true);
            bullet.transform.position = _attackPoint.Position;
            bullet.transform.rotation = _attackPoint.Rotation;

            return bullet;
        }

        public void Put(Bullet bullet) => _bullets.Enqueue(bullet);

        private class BulletFactory
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
}