using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Source.Code_base
{
    public class EnemySpawner : MonoBehaviour
    {

        [SerializeField][Range(0.1f, 20f)] private float _ufoSpawnCooldown;
        [SerializeField][Range(0.1f, 20f)] private float _asteroidSpawnCooldown;

        private IEnemyFactory _factory;
        private ObjectPool<Enemy> _enemyPool;
        private EnemyManager _enemyController;
        private PauseController _pauseController;

        private WaitForSecondsRealtime _ufoSpawnDelay;
        private WaitForSecondsRealtime _asteroidSpawnDelay;

        private Transform _transform;

        public void Init(Transform character, EnemyManager enemyManager, PauseController pauseController, IEnemyFactory enemyFactory)
        {
            _enemyPool = new();
            _factory = enemyFactory;
            _enemyController = enemyManager;
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
            if (_enemyPool.TryGet(out Enemy enemy))
            {
                _enemyController.AddEnemy(enemy);
            }
            else
            {
                enemy = _factory.Create(name);
            }

            enemy.transform.position = spawnPosition;

            if (enemy.Name != EnemyNames.UFO)
                enemy.transform.rotation = Quaternion.Euler(GetRandomEuler2D());
        }

        private Vector3 GetRandomEuler2D()
        {
            float randomZ = Random.Range(0f, 360f);
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