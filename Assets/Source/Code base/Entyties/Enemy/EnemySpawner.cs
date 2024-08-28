using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Source.Code_base
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private EnemyFactory _factory;

        [SerializeField][Range(0.1f, 20f)] private float _ufoSpawnCooldown;
        [SerializeField][Range(0.1f, 20f)] private float _asteroidSpawnCooldown;

        private EnemyPool _enemyPool;
        private EnemyManager _lifeController;
        private PauseController _pauseController;

        private Transform _transform;

        public void Init(Transform character, EnemyManager enemyManager, PauseController pauseController)
        {
            _factory.Init(character);
            _enemyPool = new(_factory);
            _lifeController = enemyManager;
            _pauseController = pauseController;
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
            Enemy enemy = _enemyPool.Get(name);
            _lifeController.AddEnemy(enemy);
            enemy.transform.position = spawnPosition;

            if(enemy.Name != EnemyNames.UFO)
                enemy.transform.rotation = Quaternion.Euler(GetRandomEuler2D());
        }

        private Vector3 GetRandomEuler2D()
        {
            float randomZ = Random.Range(0f, 360f);

            return new Vector3(0, 0, randomZ);
        }

        private IEnumerator StartSpawnUfo()
        {
            WaitForSecondsRealtime delay = new(_ufoSpawnCooldown);

            while (enabled)
            {
                yield return delay;

                if (_pauseController.IsPause == false)
                    SpawnEnemy(EnemyNames.UFO, _transform.position);
            }
        }

        private IEnumerator StartSpawnAsteroid()
        {
            WaitForSecondsRealtime delay = new(_asteroidSpawnCooldown);

            while (enabled)
            {
                yield return delay;

                if (_pauseController.IsPause == false)
                    SpawnEnemy(EnemyNames.AsteroidBig, _transform.position);
            }
        }
    }
}