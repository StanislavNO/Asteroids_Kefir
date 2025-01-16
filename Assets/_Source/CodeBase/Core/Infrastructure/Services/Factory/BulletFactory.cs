using Assets._Source.CodeBase.Core.Common.Configs;
using Assets._Source.CodeBase.Core.Gameplay.Enemies;
using Assets._Source.CodeBase.Core.Infrastructure.Services.Input;
using System;
using System.Collections.Generic;

namespace Assets._Source.CodeBase.Core.Infrastructure.Services.Factory
{
    public class BulletFactory : IBulletFactory, IDisposable
    {
        private readonly PrefabsConfig _prefabs;
        private readonly Pool<Bullet> _bullets;
        private readonly List<Bullet> _activeBullets;

        private AttackPoint _attackPoint;

        public BulletFactory(PrefabsConfig prefabsConfig)
        {
            _prefabs = prefabsConfig;

            _bullets = new Pool<Bullet>(Create);
            _activeBullets = new List<Bullet>();
        }

        public void Init(AttackPoint attackPoint)
        {
            _attackPoint = attackPoint;
        }

        public void Dispose()
        {
            if (_activeBullets.Count == 0)
                return;

            foreach (Bullet bullet in _activeBullets)
                bullet.AttackComplied -= OnBulletDeactivated;
        }

        public Bullet Get()
        {
            Bullet bullet = _bullets.Get();

            bullet.transform.SetPositionAndRotation(
                _attackPoint.Position, _attackPoint.Rotation);

            _activeBullets.Add(bullet);

            bullet.AttackComplied += OnBulletDeactivated;

            return bullet;
        }

        private Bullet Create()
        {
            return UnityEngine.Object.Instantiate(
                    _prefabs.DefaultBulletPrefab,
                    _attackPoint.Position,
                    _attackPoint.Rotation);
        }

        private void OnBulletDeactivated(Bullet bullet)
        {
            bullet.AttackComplied -= OnBulletDeactivated;

            _activeBullets.Remove(bullet);
            _bullets.Put(bullet);
        }
    }
}