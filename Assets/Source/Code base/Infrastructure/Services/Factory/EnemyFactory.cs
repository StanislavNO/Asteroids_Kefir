using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Source.Code_base
{
    public class EnemyFactory : IEnemyFactory
    {
        private readonly PrefabsConfig _prefabs;
        private readonly PauseController _pauseController;
        private readonly Transform _character;

        private readonly Pool<Asteroid> _asteroidBigPool;
        private readonly Pool<MiniAsteroid> _asteroidMiniPool;
        private readonly Pool<CharacterFollower> _ufoPool;
        private readonly List<Enemy> _activeEnemies;

        private Enemy _enemy;

        public EnemyFactory(PrefabsConfig prefabs, PauseController pauseController, Transform character)
        {
            _prefabs = prefabs;
            _pauseController = pauseController;
            _character = character;

            _asteroidBigPool = new(CreateAsteroidBig);
            _asteroidMiniPool = new(CreateAsteroidMini);
            _ufoPool = new(CreateUfo);

            _activeEnemies = new();
        }

        public void Destroy()
        {
            if (_activeEnemies.Count == 0)
                return;

            foreach (Enemy enemy in _activeEnemies)
                enemy.Died -= OnDieEnemy;
        }

        public Enemy Get(EnemyNames name)
        {
            switch (name)
            {
                case EnemyNames.AsteroidBig:
                    _enemy = _asteroidBigPool.Get();
                    break;

                case EnemyNames.AsteroidMini:
                    _enemy = _asteroidMiniPool.Get();
                    break;

                case EnemyNames.UFO:
                    _enemy = _ufoPool.Get();
                    break;

                default: return null;
            }

            RegistrationEnemy();

            return _enemy;
        }

        private void OnDieEnemy(Enemy enemy)
        {
            UnRegistrationEnemy(enemy);

            switch (enemy.Name)
            {
                case EnemyNames.AsteroidBig:
                    _asteroidBigPool.Put(enemy as Asteroid);
                    break;

                case EnemyNames.AsteroidMini:
                    _asteroidMiniPool.Put(enemy as MiniAsteroid);
                    break;

                case EnemyNames.UFO:
                    _ufoPool.Put(enemy as CharacterFollower);
                    break;
            }
        }

        private void RegistrationEnemy()
        {
            _activeEnemies.Add(_enemy);
            _enemy.Died += OnDieEnemy;
        }

        private void UnRegistrationEnemy(Enemy enemy)
        {
            _activeEnemies.Remove(enemy);
            enemy.Died -= OnDieEnemy;
        }

        private Asteroid CreateAsteroidBig()
        {
            Asteroid enemy = Object.Instantiate(_prefabs.EnemyPrefabs.AsteroidBig);
            enemy.Init(_pauseController);
            return enemy;
        }

        private MiniAsteroid CreateAsteroidMini()
        {
            MiniAsteroid enemy = Object.Instantiate(_prefabs.EnemyPrefabs.AsteroidMini);
            enemy.Init(_pauseController);
            return enemy;
        }

        private CharacterFollower CreateUfo()
        {
            CharacterFollower enemy = Object.Instantiate(_prefabs.EnemyPrefabs.Ufo);
            enemy.SetTarget(_character);
            enemy.Init(_pauseController);
            return enemy;
        }
    }
}