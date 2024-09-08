using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Source.Code_base
{
    public class Factory : IFactory
    {
        private readonly AttackPoint _attackPoint;
        private readonly PrefabsConfig _prefabs;
        private readonly Transform _character;
        private readonly PauseController _pauseController;

        private readonly ObjectPool<Bullet> _bullets;
        private readonly List<Bullet> _activeBullets;

        public Factory(AttackPoint attackPoint, PrefabsConfig prefabsConfig, Transform character, PauseController pauseController)
        {
            _attackPoint = attackPoint;
            _prefabs = prefabsConfig;
            _character = character;
            _pauseController = pauseController;

            _bullets = new ObjectPool<Bullet>();
            _activeBullets = new List<Bullet>();
        }

        public void Destroy()
        {
            if (_activeBullets.Count > 0)
            {
                foreach (Bullet bullet in _activeBullets)
                    bullet.AttackComplied -= OnBulletDeactivated;
            }
        }

        public Bullet Create()
        {
            Bullet bullet;

            if (_bullets.TryGet(out bullet))
            {
                bullet.transform.position = _attackPoint.Position;
                bullet.transform.rotation = _attackPoint.Rotation;
            }
            else
            {
                bullet = Object.Instantiate(
                    _prefabs.DefaultBulletPrefab,
                    _attackPoint.Position,
                    _attackPoint.Rotation);
            }

            _activeBullets.Add(bullet);
            bullet.AttackComplied += OnBulletDeactivated;
            return bullet;
        }

        public Enemy Create(EnemyNames name)
        {
            switch (name)
            {
                case EnemyNames.AsteroidBig:
                    return CreateAsteroid(_prefabs.EnemyPrefabs.AsteroidBig);

                case EnemyNames.AsteroidMini:
                    return CreateAsteroid(_prefabs.EnemyPrefabs.AsteroidMini);

                case EnemyNames.UFO:
                    return CreateUfo();

                default: return null;
            }
        }

        private void OnBulletDeactivated(Bullet bullet)
        {
            _activeBullets.Remove(bullet);
            bullet.AttackComplied -= OnBulletDeactivated;
            _bullets.Put(bullet);
        }

        private Enemy CreateAsteroid(Enemy prefab)
        {
            Enemy enemy = Object.Instantiate(prefab);
            enemy.Init(_pauseController);
            return enemy;
        }

        private Enemy CreateUfo()
        {
            Enemy enemy = Object.Instantiate(_prefabs.EnemyPrefabs.Ufo);
            enemy.GetComponent<CharacterFollower>().SetTarget(_character);
            enemy.Init(_pauseController);
            return enemy;
        }
    }
}