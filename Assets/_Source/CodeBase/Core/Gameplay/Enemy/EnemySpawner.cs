using Assets._Source.CodeBase.Core.Common;
using Assets._Source.CodeBase.Core.Infrastructure.Services.Factory;
using Assets._Source.CodeBase.Core.Infrastructure.Services.TimeManager;
using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets._Source.CodeBase.Core.Gameplay.Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        public event Action<Enemy> OnSpawned;

        [SerializeField][Range(0.1f, 20f)] private float _ufoSpawnCooldown;
        [SerializeField][Range(0.1f, 20f)] private float _asteroidSpawnCooldown;

        private IEnemyFactory _factory;
        private IReadOnlyPause _pauseController;

        private WaitForSecondsRealtime _ufoSpawnDelay;
        private WaitForSecondsRealtime _asteroidSpawnDelay;

        private Transform _transform;

        [Inject]
        private void Construct(IReadOnlyPause pauseController, IEnemyFactory enemyFactory)
        {
            _transform = transform;
            _factory = enemyFactory;
            _pauseController = pauseController;

            _ufoSpawnDelay = new(_ufoSpawnCooldown);
            _asteroidSpawnDelay = new(_asteroidSpawnCooldown);
        }

        private void Start()
        {
            StartCoroutine(StartSpawnAsteroid());
            StartCoroutine(StartSpawnUfo());
        }

        public void SpawnEnemy(EnemyNames name, Vector3 spawnPosition)
        {
            Enemy enemy = _factory.Get(name);

            enemy.transform.position = spawnPosition;
            OnSpawned?.Invoke(enemy);

            if (enemy.Name != EnemyNames.UFO)
            {
                Vector3 randomEuler = Utilities.GetRandomEulerZ();
                enemy.transform.rotation = Quaternion.Euler(randomEuler);
            }
        }

        private IEnumerator StartSpawnUfo()
        {
            while (enabled)
            {
                yield return _ufoSpawnDelay;

                if (_pauseController.IsPause == false)
                    SpawnEnemy(EnemyNames.UFO, _transform.position);
            }
        }

        private IEnumerator StartSpawnAsteroid()
        {
            while (enabled)
            {
                yield return _asteroidSpawnDelay;

                if (_pauseController.IsPause == false)
                    SpawnEnemy(EnemyNames.AsteroidBig, _transform.position);
            }
        }
    }
}