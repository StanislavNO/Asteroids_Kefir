using Assets._Source.CodeBase.Core.Infrastructure.Services.Score;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Source.CodeBase.Core.Gameplay.Enemies
{
    public class EnemyManager : IDisposable, IEnemyDieSignal
    {
        public event Action AsteroidDie;
        public event Action UFODie;

        private readonly ScoreManager _scoreManager;
        private readonly EnemySpawner _enemySpawner;
        private readonly List<Enemy> _activeEnemies;
        private readonly EnemySpawner _spawner;

        public EnemyManager(ScoreManager scoreManager, EnemySpawner enemySpawner)
        {
            _spawner = enemySpawner;
            _enemySpawner = enemySpawner;
            _scoreManager = scoreManager;
            _activeEnemies = new List<Enemy>();

            _spawner.Spawning += OnEnemySpawning;
        }

        public void Dispose()
        {
            _spawner.Spawning -= OnEnemySpawning;

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
            if (enemy.Name == EnemyNames.UFO)
                UFODie?.Invoke();
            else
                AsteroidDie?.Invoke();

            enemy.Died -= OnEnemyDied;
            _activeEnemies.Remove(enemy);

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