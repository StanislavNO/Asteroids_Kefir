using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Source.Code_base
{
    public class EnemyManager
    {
        private readonly ScoreManager _scoreManager;

        private EnemyPool _enemyPool;
        private List<Enemy> _activeEnemies;
        private EnemySpawner _enemySpawner;

        public EnemyManager(ScoreManager scoreManager)
        {
            if (scoreManager is null)
                throw new ArgumentNullException(nameof(scoreManager));

            _scoreManager = scoreManager;
            _activeEnemies = new List<Enemy>();
        }

        public void Destroy()
        {
            if (_activeEnemies is not null && _activeEnemies.Count > 0)
            {
                foreach (Enemy enemy in _activeEnemies)
                    enemy.Died -= OnEnemyDied;
            }
        }

        public void Initialize(EnemyPool enemyPool, EnemySpawner enemySpawner)
        {
            if (enemyPool is null)
                throw new ArgumentNullException(nameof(enemyPool));
            if (enemySpawner is null)
                throw new ArgumentNullException(nameof(enemySpawner));

            if (_enemyPool is null)
                _enemyPool = enemyPool;

            if (_enemySpawner is null)
                _enemySpawner = enemySpawner;
        }

        public void AddEnemy(Enemy enemy)
        {
            if(enemy is null)
                throw new ArgumentNullException($"{nameof(enemy)} is null");

            _activeEnemies.Add(enemy);
            enemy.Died += OnEnemyDied;
        }

        private void OnEnemyDied(Enemy enemy)
        {
            if(enemy is null)
                throw new ArgumentNullException($"{nameof(enemy)} is null");

            _activeEnemies.Remove(enemy);
            enemy.Died -= OnEnemyDied;

            _enemyPool.Put(enemy);
            _scoreManager.Add(enemy.Reward);

            if (enemy.Name == EnemyNames.AsteroidBig)
                OnBigDied(enemy.transform.position);
        }

        private void OnBigDied(Vector3 deathPosition)
        {
            float offset = 0.5f;

            Vector2 left = new(deathPosition.x - offset, deathPosition.y);
            Vector2 right = new(deathPosition.x + offset, deathPosition.y);
            Vector2 up = new(deathPosition.x , deathPosition.y + offset);
            Vector2 down = new(deathPosition.x , deathPosition.y - offset);

            _enemySpawner.SpawnEnemy(EnemyNames.AsteroidMini, left);
            _enemySpawner.SpawnEnemy(EnemyNames.AsteroidMini, right);
            _enemySpawner.SpawnEnemy(EnemyNames.AsteroidMini, up);
            _enemySpawner.SpawnEnemy(EnemyNames.AsteroidMini, down);
        }
    }
}