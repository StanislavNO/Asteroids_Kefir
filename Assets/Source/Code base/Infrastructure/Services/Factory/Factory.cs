using UnityEngine;
using System;
using Object = UnityEngine.Object;

namespace Assets.Source.Code_base
{
    public class Factory : IFactory
    {
        private readonly WeaponConfig _weaponStat;
        private readonly AttackPoint _attackPoint;
        private readonly PrefabsConfig _prefabs;
        private readonly Transform _character;
        private readonly PauseController _pauseController;

        public Factory(WeaponConfig weaponStat, AttackPoint attackPoint, PrefabsConfig prefabsConfig, Transform character, PauseController pauseController)
        {
            if (prefabsConfig == null)
                throw new ArgumentNullException(nameof(prefabsConfig));

            _weaponStat = weaponStat;
            _attackPoint = attackPoint;
            _prefabs = prefabsConfig;
            _character = character;
            _pauseController = pauseController;
        }

        public Bullet Create()
        {
            Bullet bullet = GameObject.Instantiate(
                _prefabs.DefaultBulletPrefab,
                _attackPoint.Position,
                _attackPoint.Rotation);

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
