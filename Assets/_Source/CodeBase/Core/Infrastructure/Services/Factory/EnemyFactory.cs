using Assets._Source.CodeBase.Core.Common.Configs;
using Assets._Source.CodeBase.Core.Gameplay.Enemies;
using Assets._Source.CodeBase.Core.Infrastructure.Services.Input;
using System;
using System.Collections.Generic;
using Zenject;

namespace Assets._Source.CodeBase.Core.Infrastructure.Services.Factory
{
    public class EnemyFactory : IEnemyFactory, IDisposable
    {
        private readonly IInitializer _initializer;

        private readonly Pool<Asteroid> _asteroidBigPool;
        private readonly Pool<MiniAsteroid> _asteroidMiniPool;
        private readonly Pool<CharacterFollower> _ufoPool;
        private readonly List<Enemy> _activeEnemies;

        public EnemyFactory(PrefabsConfig prefabs, IInitializer initializer)
        {
            _initializer = initializer;

            _asteroidBigPool = new(() => Create(prefabs.EnemyPrefabs.AsteroidBig));
            _asteroidMiniPool = new(() => Create(prefabs.EnemyPrefabs.AsteroidMini));
            _ufoPool = new(() => Create(prefabs.EnemyPrefabs.Ufo));

            _activeEnemies = new();
        }

        public void Dispose()
        {
            foreach (Enemy enemy in _activeEnemies)
                enemy.OnDied -= OnEnemyDied;
        }

        public Enemy Get(EnemyNames name)
        {
            Enemy enemy;

            switch (name)
            {
                case EnemyNames.AsteroidBig:
                    enemy = _asteroidBigPool.Get();
                    break;

                case EnemyNames.AsteroidMini:
                    enemy = _asteroidMiniPool.Get();
                    break;

                case EnemyNames.UFO:
                    enemy = _ufoPool.Get();
                    break;

                default:
                    return null;
            }

            RegistrationEnemy(enemy);

            return enemy;
        }

        private void OnEnemyDied(Enemy enemy)
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

        private void RegistrationEnemy(Enemy enemy)
        {
            enemy.OnDied += OnEnemyDied;
            _activeEnemies.Add(enemy);
        }

        private void UnRegistrationEnemy(Enemy enemy)
        {
            enemy.OnDied -= OnEnemyDied;
            _activeEnemies.Remove(enemy);
        }

        private T Create<T>(T prefab) where T : Enemy =>
            _initializer.InstantiatePrefabForComponent<T>(prefab);
    }
}