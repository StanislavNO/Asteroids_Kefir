using System.Collections.Generic;
using UnityEngine;

namespace Assets.Source.Code_base
{
    public class BulletFactory : IBulletFactory
    {
        private readonly AttackPoint _attackPoint;
        private readonly PrefabsConfig _prefabs;

        private readonly Pool<Bullet> _bullets;
        private readonly List<Bullet> _activeBullets;

        public BulletFactory(AttackPoint attackPoint, PrefabsConfig prefabsConfig)
        {
            _attackPoint = attackPoint;
            _prefabs = prefabsConfig;

            _bullets = new Pool<Bullet>(Create);
            _activeBullets = new List<Bullet>();
        }

        public void Destroy()
        {
            if (_activeBullets.Count == 0)
                return;

            foreach (Bullet bullet in _activeBullets)
                bullet.AttackComplied -= OnBulletDeactivated;
        }

        public Bullet Get()
        {
            Bullet bullet = _bullets.Get();

            bullet.transform.position = _attackPoint.Position;
            bullet.transform.rotation = _attackPoint.Rotation;
            _activeBullets.Add(bullet);

            bullet.AttackComplied += OnBulletDeactivated;

            return bullet;
        }

        private Bullet Create()
        {
            return Object.Instantiate(
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