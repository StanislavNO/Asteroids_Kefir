using System;
using System.Collections;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Assets.Source.Code_base
{
    public class EnemySpawner : MonoBehaviour
    {
        public event Action<Enemy> Spawning;

        [SerializeField][Range(0.1f, 20f)] private float _ufoSpawnCooldown;
        [SerializeField][Range(0.1f, 20f)] private float _asteroidSpawnCooldown;

        private IEnemyFactory _factory;
        private PauseController _pauseController;

        private WaitForSecondsRealtime _ufoSpawnDelay;
        private WaitForSecondsRealtime _asteroidSpawnDelay;

        private Transform _transform;

        [Inject]
        private void Construct(PauseController pauseController, IEnemyFactory enemyFactory)
        {
            _factory = enemyFactory;
            _pauseController = pauseController;

            _ufoSpawnDelay = new(_ufoSpawnCooldown);
            _asteroidSpawnDelay = new(_asteroidSpawnCooldown);
        }

        private void Awake()
        {
            _transform = transform;
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
            Spawning?.Invoke(enemy);

            if (enemy.Name != EnemyNames.UFO)
                enemy.transform.rotation = Quaternion.Euler(GetRandomEuler2D());
        }

        private Vector3 GetRandomEuler2D()
        {
            float minAngle = 0f;
            float maxAngle = 360f;

            float randomZ = Random.Range(minAngle, maxAngle);
            Vector3 randomVector = Vector3.forward * randomZ;

            return randomVector;
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