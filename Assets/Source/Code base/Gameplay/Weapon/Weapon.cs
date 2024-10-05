using System;
using System.Collections;
using UnityEngine;

namespace Assets.Source.Code_base
{
    public class Weapon : IReadOnlyWeapon , IWeapon, IDisposable
    {
        public event Action LaserRecharging;
        public event Action<int> LaserBulletChanged;
        public event Action Attacking;

        private readonly IInputAttacker _input;
        private readonly IBulletFactory _bulletFactory;

        private readonly int _startLaserBulletCount;
        private readonly WaitForSecondsRealtime _timeWorkLaser;
        private readonly WaitForSecondsRealtime _timeRechargeLaser;

        private GameObject _laser;
        private ICoroutineRunner _coroutineRunner;
        private bool _isLaserCooldown = false;

        public int LaserBulletCount { get; private set; }
        public float LaserCooldown { get; private set; }

        public Weapon(IInputAttacker input, CharacterConfig config, IBulletFactory bulletFactory)
        {
            _input = input;
            _bulletFactory = bulletFactory;

            _startLaserBulletCount = config.Weapon.LaserBulletCount;
            LaserBulletCount = config.Weapon.LaserBulletCount;
            LaserCooldown = config.Weapon.LaserCooldown;

            _timeRechargeLaser = new(LaserCooldown);
            _timeWorkLaser = new(config.Weapon.TimeWorkLaser);

            _input.DefaultAttacking += AttackDefold;
            _input.HardAttacking += AttackLaser;
        }

        public void Dispose()
        {
            _input.DefaultAttacking -= AttackDefold;
            _input.HardAttacking -= AttackLaser;
        }

        public void Init(AttackPoint attackPoint, ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
            _laser = attackPoint.LaserBullet;
            _bulletFactory.Init(attackPoint);
        }

        private void AttackLaser()
        {
            if (!_laser.activeSelf && LaserBulletCount > 0)
            {
                LaserBulletChanged?.Invoke(--LaserBulletCount);
                _coroutineRunner.StartCoroutine(ActivateLaser());
            }

            if (_isLaserCooldown == false && LaserBulletCount == 0)
                _coroutineRunner.StartCoroutine(RechargeLaser());
        }

        private void AttackDefold()
        {
            _bulletFactory.Get();
            Attacking?.Invoke();
        }

        private IEnumerator ActivateLaser()
        {
            _laser.SetActive(true);

            yield return _timeWorkLaser;

            _laser.SetActive(false);
        }

        private IEnumerator RechargeLaser()
        {
            LaserRecharging?.Invoke();
            _isLaserCooldown = true;

            yield return _timeRechargeLaser;

            LaserBulletCount = _startLaserBulletCount;
            LaserBulletChanged?.Invoke(LaserBulletCount);
            _isLaserCooldown = false;
        }
    }
}