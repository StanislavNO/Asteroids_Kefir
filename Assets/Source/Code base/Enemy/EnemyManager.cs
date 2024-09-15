using System.Collections.Generic;
using UnityEngine;

namespace Assets.Source.Code_base
{
    public class EnemyManager
    {
        private readonly ScoreManager _scoreManager;
        private readonly List<Enemy> _activeEnemies;
        private readonly EnemySpawner _enemySpawner;

        public EnemyManager(ScoreManager scoreManager, EnemySpawner enemySpawner)
        {
            _enemySpawner = enemySpawner;
            _scoreManager = scoreManager;
            _activeEnemies = new List<Enemy>();
        }

        public void Destroy()
        {
            if (_activeEnemies.Count > 0)
            {
                foreach (Enemy enemy in _activeEnemies)
                    enemy.Died -= OnEnemyDied;
            }
        }

        public void AddEnemy(Enemy enemy)
        {
            _activeEnemies.Add(enemy);
            enemy.Died += OnEnemyDied;
        }

        private void OnEnemyDied(Enemy enemy)
        {
            _activeEnemies.Remove(enemy);
            enemy.Died -= OnEnemyDied;

            _scoreManager.Add(enemy.Reward);

            if (enemy.Name == EnemyNames.AsteroidBig)
                OnBigDied(enemy.transform.position);
        }

        private void OnBigDied(Vector3 deathPosition)
        {
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