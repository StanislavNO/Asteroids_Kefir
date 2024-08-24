using System;
using System.Collections;
using UnityEngine;

namespace Assets.Source.Code_base
{
    public class Weapon : IReadOnlyWeapon
    {
        private readonly IInputAttacker _input;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly AttackPoint _attackPoint;
        private readonly BulletPool _pool;
        private readonly GameObject _laser;

        private readonly int _startLaserBulletCount;
        private bool IsLaserCooldown = false;

        public Weapon(IInputAttacker input, WeaponConfig weaponStat, ICoroutineRunner coroutineRunner, AttackPoint attackPoint)
        {
            _input = input;
            _coroutineRunner = coroutineRunner;
            _attackPoint = attackPoint;
            _pool = new BulletPool(weaponStat, _attackPoint);
            _laser = attackPoint.LaserBullet;

            _startLaserBulletCount = weaponStat.LaserBulletCount;
            LaserBulletCount = _startLaserBulletCount;
            LaserCooldown = weaponStat.LaserCooldown;

            _input.DefoldAttacking += AttackDefold;
            _input.HardAttacking += AttackLaser;
        }

        public event Action LaserRecharging;
        public event Action<int> LaserBulletChanged;

        public int LaserBulletCount { get; private set; }
        public float LaserCooldown { get; private set; }

        public void Destroy()
        {
            _input.DefoldAttacking -= AttackDefold;
            _input.HardAttacking -= AttackLaser;
        }

        private void AttackLaser()
        {
            if (!_laser.activeSelf && LaserBulletCount > 0)
            {
                LaserBulletChanged?.Invoke(--LaserBulletCount);
                _coroutineRunner.StartCoroutine(ActivateLaser());
            }

            if (IsLaserCooldown == false && LaserBulletCount == 0)
                _coroutineRunner.StartCoroutine(RechargeLaser());
        }

        private void AttackDefold()
        {
            _pool.Get();
        }

        private IEnumerator ActivateLaser()
        {
            WaitForSecondsRealtime delay = new(1f);

            _laser.SetActive(true);
            yield return delay;
            _laser.SetActive(false);
        }

        private IEnumerator RechargeLaser()
        {
            WaitForSecondsRealtime delay = new(LaserCooldown);

            LaserRecharging?.Invoke();
            IsLaserCooldown = true;

            yield return delay;

            LaserBulletCount = _startLaserBulletCount;
            LaserBulletChanged?.Invoke(LaserBulletCount);
            IsLaserCooldown = false;
        }
    }
}