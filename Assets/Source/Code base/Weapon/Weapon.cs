﻿using System;
using System.Collections;
using UnityEngine;

namespace Assets.Source.Code_base
{
    public class Weapon : IReadOnlyWeapon , IWeapon
    {
        public event Action LaserRecharging;
        public event Action<int> LaserBulletChanged;
        public event Action Attacking;

        private readonly IInputAttacker _input;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IBulletFactory _factoryBullet;
        private readonly GameObject _laser;
        private readonly WeaponAudioController _audioController;

        private readonly int _startLaserBulletCount;
        private readonly WaitForSecondsRealtime _timeWorkLaser;
        private readonly WaitForSecondsRealtime _timeRechargeLaser;

        private bool _isLaserCooldown = false;

        public int LaserBulletCount { get; private set; }
        public float LaserCooldown { get; private set; }

        public Weapon(IInputAttacker input, WeaponConfig weaponStat, ICoroutineRunner coroutineRunner, AttackPoint attackPoint, WeaponAudioController audioController, IBulletFactory bulletFactory)
        {
            _input = input;
            _coroutineRunner = coroutineRunner;
            _laser = attackPoint.LaserBullet;
            _audioController = audioController;
            _factoryBullet = bulletFactory;

            _startLaserBulletCount = weaponStat.LaserBulletCount;
            LaserBulletCount = weaponStat.LaserBulletCount;
            LaserCooldown = weaponStat.LaserCooldown;

            _timeRechargeLaser = new(weaponStat.LaserCooldown);
            _timeWorkLaser = new(weaponStat.TimeWorkLaser);

            _input.DefaultAttacking += AttackDefold;
            _input.HardAttacking += AttackLaser;
        }

        public void Destroy()
        {
            _input.DefaultAttacking -= AttackDefold;
            _input.HardAttacking -= AttackLaser;
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
            Attacking?.Invoke();
            _factoryBullet.Get();
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