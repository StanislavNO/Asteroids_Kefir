using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Source.Code_base
{
    public class EnemySpawner : MonoBehaviour, IPause
    {
        [SerializeField] private EnemyFactory _factory;

        [SerializeField][Range(0.1f, 20f)] private float _ufoSpawnCooldown;
        [SerializeField][Range(0.1f, 20f)] private float _asteroidSpawnCooldown;

        private EnemyPool _enemyPool;
        private EnemyManager _lifeController;

        private bool _isPause;
        private Transform _transform;

        public void Init(Transform character, EnemyManager enemyManager)
        {
            CheckForNull(character, enemyManager);

            _factory.Init(character);
            _enemyPool = new(_factory);
            _lifeController = enemyManager;
            _lifeController.Initialize(_enemyPool, this);

            _isPause = false;
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

        public void Pause(bool isPause) => _isPause = isPause;

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

                if (_isPause == false)
                    SpawnEnemy(EnemyNames.UFO, _transform.position);
            }
        }

        private IEnumerator StartSpawnAsteroid()
        {
            WaitForSecondsRealtime delay = new(_asteroidSpawnCooldown);

            while (enabled)
            {
                yield return delay;

                if (_isPause == false)
                    SpawnEnemy(EnemyNames.AsteroidBig, _transform.position);
            }
        }

        private static void CheckForNull(Transform character, EnemyManager enemyLifeController)
        {
            if (character is null)
            {
                throw new ArgumentNullException(nameof(character));
            }

            if (enemyLifeController is null)
            {
                throw new ArgumentNullException(nameof(enemyLifeController));
            }
        }
    }
}