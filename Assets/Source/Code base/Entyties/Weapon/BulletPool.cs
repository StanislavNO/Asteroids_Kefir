using System;
using UnityEngine;

namespace Assets.Source.Code_base
{
    public class BulletPool
    {
        private readonly WeaponConfig _weaponStat;

        public BulletPool(WeaponConfig weaponStat)
        {
            _weaponStat = weaponStat;
        }

        public Bullet Get()
        {
            return GameObject.Instantiate(_weaponStat.DefoldBulletPrefab);
        }
    }
}