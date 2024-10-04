using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Source.Code_base
{
    public class EnemyManager : IDisposable
    {
        private readonly ScoreManager _scoreManager;
        private readonly List<Enemy> _activeEnemies;
        private readonly EnemySpawner _enemySpawner;

        public EnemyManager(ScoreManager scoreManager, EnemySpawner enemySpawner)
        {
            _enemySpawner = enemySpawner;
            _scoreManager = scoreManager;
            _activeEnemies = new List<Enemy>();

            enemySpawner.Spawning += OnEnemySpawning;
        }

        public void Dispose()
        {
            if (_activeEnemies.Count == 0)
                return;

            foreach (Enemy enemy in _activeEnemies)
                enemy.Died -= OnEnemyDied;
        }

        private void OnEnemySpawning(Enemy enemy)
        {
            enemy.Died += OnEnemyDied;
            _activeEnemies.Add(enemy);
        }

        private void OnEnemyDied(Enemy enemy)
        {
            Debug.Log("OnEnemyDied " + enemy.Name);
            enemy.Died -= OnEnemyDied;
            _activeEnemies.Remove(enemy);

            _scoreManager.Add(enemy.Reward);

            if (enemy.Name == EnemyNames.AsteroidBig)
                OnBigDied(enemy.transform.position);
        }

        private void OnBigDied(Vector3 deathPosition)
        {
            Debug.Log("spawn mini");
            float offset = 0.5f;

            Vector2 left = new(deathPosition.x - offset, deathPosition.y);
            Vector2 right = new(deathPosition.x + offset, deathPosition.y);
            Vector2 up = new(deathPosition.x, deathPosition.y + offset);
            Vector2 down = new(deathPosition.x, deathPosition.y - offset);

            _enemySpawner.SpawnEnemy(EnemyNames.AsteroidMini, left);
            _enemySpawner.SpawnEnemy(EnemyNames.AsteroidMini, right);
            _enemySpawner.SpawnEnemy(EnemyNames.AsteroidMini, up);
            _enemySpawner.SpawnEnemy(EnemyNames.AsteroidMini, down);
        }
    }
}