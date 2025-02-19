using Assets._Source.CodeBase.Core.Common.Configs;
using Assets._Source.CodeBase.Core.Infrastructure.Services.Factory;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace Assets._Source.CodeBase.Core.Gameplay.Enemies
{
    public class Weapon : IWeapon, IWeaponInitializator, IAttackObserver
    {
        public event Action<float> OnLaserAttacking;
        public event Action OnDefaultAttacked;

        public event Action<float> OnLaserRecharging;
        public event Action<int> OnLaserBulletChanged;

        private readonly IBulletFactory _bulletFactory;
        private readonly int _startLaserBulletCount;
        private readonly float _timeRechargeLaser;

        private GameObject _laser;
        private bool _isLaserCooldown = false;

        public float TimeWorkLaser { get; private set; }
        public int LaserBulletCount { get; private set; }
        public float LaserCooldown { get; private set; }

        public Weapon(CharacterConfig config, IBulletFactory bulletFactory)
        {
            _bulletFactory = bulletFactory;

            _startLaserBulletCount = config.Weapon.LaserBulletCount;
            LaserBulletCount = _startLaserBulletCount;
            LaserCooldown = config.Weapon.LaserCooldown;

            _timeRechargeLaser = LaserCooldown;
            TimeWorkLaser = config.Weapon.TimeWorkLaser;
        }

        public void Init(AttackPoint attackPoint)
        {
            _laser = attackPoint.LaserBullet;
            _bulletFactory.Init(attackPoint);
        }

        public bool TryAttackLaser()
        {
            if (_laser.activeSelf == true || _isLaserCooldown)
                return false;

            LaserBulletCount--;
            ActivateLaser();

            if (LaserBulletCount == 0)
                RechargeLaser();

            return true;
        }

        public void AttackDefault()
        {
            _bulletFactory.Get();
            OnDefaultAttacked?.Invoke();
        }

        private async void ActivateLaser()
        {
            _laser.SetActive(true);

            OnLaserAttacking?.Invoke(TimeWorkLaser);
            await UniTask.Delay(TimeSpan.FromSeconds(TimeWorkLaser));

            _laser.SetActive(false);
        }

        private async void RechargeLaser()
        {
            _isLaserCooldown = true;
            OnLaserRecharging?.Invoke(_timeRechargeLaser);

            await UniTask.Delay(TimeSpan.FromSeconds(_timeRechargeLaser), false);

            LaserBulletCount = _startLaserBulletCount;
            OnLaserBulletChanged?.Invoke(LaserBulletCount);
            _isLaserCooldown = false;
        }
    }
}