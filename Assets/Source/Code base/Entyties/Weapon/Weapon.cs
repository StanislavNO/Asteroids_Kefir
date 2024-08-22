using System;
using UnityEngine;

namespace Assets.Source.Code_base
{
    public class Weapon : IReadOnlyWeapon
    {
        private readonly IInputAttacker _input;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly AttackPoint _attackPoint;
        private readonly BulletPool _pool;

        public Weapon(IInputAttacker input, WeaponConfig weaponStat, ICoroutineRunner coroutineRunner, AttackPoint attackPoint)
        {
            _input = input;
            _coroutineRunner = coroutineRunner;
            _attackPoint = attackPoint;
            _pool = new BulletPool(weaponStat);

            LaserBullet = weaponStat.LaserBulletCount;
            LaserCooldown = weaponStat.LaserCooldown;

            _input.DefoldAttacking += AttackDefold;
            _input.HardAttacking += AttackLaser;
        }

        public event Action LaserCooldownStart;
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
            LaserCooldownStart?.Invoke();
        }

        private void AttackDefold()
        {
            Debug.Log("attack");
            Bullet bullet = _pool.Get();
            bullet.SetDirection(_attackPoint.AttackVector);
        }
    }
}