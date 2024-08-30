using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Source.Code_base
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField][Range(0.1f, 20f)] private float _ufoSpawnCooldown;
        [SerializeField][Range(0.1f, 20f)] private float _asteroidSpawnCooldown;

        private IEnemyFactory _factory;
        private EnemySpawnVisitor _spawnVisitor;
        private List<Enemy> _activeEnemies;
        private EnemyManager _enemyController;
        private PauseController _pauseController;

        private WaitForSecondsRealtime _ufoSpawnDelay;
        private WaitForSecondsRealtime _asteroidSpawnDelay;

        private Transform _transform;

        public void Init(Transform character, EnemyManager enemyManager, PauseController pauseController, IEnemyFactory enemyFactory)
        {
            _spawnVisitor = new();
            _activeEnemies = new();
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

        private void OnDestroy()
        {
            if (_activeEnemies.Count > 0)
            {
                foreach (Enemy enemy in _activeEnemies)
                    enemy.Died -= OnEnemyDie;
            }
        }

        private void Start()
        {
            StartCoroutine(StartSpawnAsteroid());
            StartCoroutine(StartSpawnUfo());
        }

        public void SpawnEnemy(EnemyNames name, Vector3 spawnPosition)
        {
            Enemy enemy;

            if (_spawnVisitor.TrySetEnemy(name))
            {
                enemy = _spawnVisitor.Enemy;
            }
            else
            {
                enemy = _factory.Create(name);
            }

            enemy.Died += OnEnemyDie;
            enemy.transform.position = spawnPosition;
            _enemyController.AddEnemy(enemy);
            _activeEnemies.Add(enemy);

            if (enemy.Name != EnemyNames.UFO)
                enemy.transform.rotation = Quaternion.Euler(GetRandomEuler2D());
        }

        private void OnEnemyDie(Enemy enemy)
        {
            enemy.Died -= OnEnemyDie;
            _activeEnemies.Remove(enemy);
            enemy.Accept(_spawnVisitor);
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