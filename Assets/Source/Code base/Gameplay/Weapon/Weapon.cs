using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace Assets.Source.Code_base
{
    public class Weapon : IReadOnlyWeapon, IWeapon
    {
        public event Action Attacking;
        public event Action LaserRecharging;
        public event Action<int> LaserBulletChanged;

        private readonly IInputAttacker _input;
        private readonly IBulletFactory _bulletFactory;

        private readonly int _startLaserBulletCount;
        private readonly float _timeWorkLaser;
        private readonly float _timeRechargeLaser;

        private GameObject _laser;
        private bool _isLaserCooldown = false;

        public int LaserBulletCount { get; private set; }
        public float LaserCooldown { get; private set; }

        public Weapon( CharacterConfig config, IBulletFactory bulletFactory)
        {
            Debug.Log("Weapon ()");
            _bulletFactory = bulletFactory;

            _startLaserBulletCount = config.Weapon.LaserBulletCount;
            LaserBulletCount = config.Weapon.LaserBulletCount;
            LaserCooldown = config.Weapon.LaserCooldown;

            _timeRechargeLaser = LaserCooldown;
            _timeWorkLaser = config.Weapon.TimeWorkLaser;
        }

        public void Init(AttackPoint attackPoint)
        {
            _laser = attackPoint.LaserBullet;
            _bulletFactory.Init(attackPoint);
        }

        public async void AttackLaser()
        {
            if (!_laser.activeSelf && LaserBulletCount > 0)
            {
                LaserBulletChanged?.Invoke(--LaserBulletCount);
                await ActivateLaser();
            }

            if (_isLaserCooldown == false && LaserBulletCount == 0)
                await RechargeLaser();
        }

        public void AttackDefold()
        {
            _bulletFactory.Get();
            Attacking?.Invoke();
        }

        private async UniTask ActivateLaser()
        {
            _laser.SetActive(true);

            await UniTask.Delay(TimeSpan.FromSeconds(_timeWorkLaser), false);

            _laser.SetActive(false);
        }

        private async UniTask RechargeLaser()
        {
            LaserRecharging?.Invoke();
            _isLaserCooldown = true;

            await UniTask.Delay(TimeSpan.FromSeconds(_timeRechargeLaser), false);

            LaserBulletCount = _startLaserBulletCount;
            LaserBulletChanged?.Invoke(LaserBulletCount);
            _isLaserCooldown = false;
        }
    }
}