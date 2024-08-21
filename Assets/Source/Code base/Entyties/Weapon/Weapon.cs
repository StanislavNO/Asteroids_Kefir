using System;
using UnityEngine;

namespace Assets.Source.Code_base
{
    public class Weapon : IReadOnlyWeapon
    {
        private readonly IInputAttacker _input;
        private readonly BulletPool _pool;

        public Weapon(IInputAttacker input, WeaponConfig weaponStat)
        {
            _input = input;
            _pool = new BulletPool(weaponStat);

            LaserBullet = weaponStat.LaserBulletCount;
            LaserCooldown = weaponStat.LaserCooldown;

            _input.DefoldAttacking += AttackDefold;
            _input.HardAttacking += AttackLaser;
        }

        public event Action<float> LaserCooldownChanged;
        public event Action<int> LaserBulletChanged;

        public int LaserBullet { get; private set; }
        public float LaserCooldown { get; private set; }

        public void Destroy()
        {
            _input.DefoldAttacking -= AttackDefold;
            _input.HardAttacking -= AttackLaser;
        }

        private void AttackLaser()
        {
            Debug.Log("Laser");
        }

        private void AttackDefold()
        {
            Debug.Log("Bullet");
        }
    }
}