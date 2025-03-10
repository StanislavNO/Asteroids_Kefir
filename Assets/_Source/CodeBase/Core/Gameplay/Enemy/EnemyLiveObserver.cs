﻿using Assets._Source.CodeBase.Core.Infrastructure.Services.Score;
using System;
using System.Collections.Generic;
using Assets._Source.CodeBase.Meta.Services.Analytics;
using UnityEngine;

namespace Assets._Source.CodeBase.Core.Gameplay.Enemies
{
    public class EnemyLiveObserver : IDisposable
    {
        public event Action<Vector2> OnDied;
        
        private readonly IStatisticsWriter _analytics;
        
        private readonly GameSessionData _gameSessionData;
        private readonly EnemySpawner _enemySpawner;
        private readonly List<Enemy> _activeEnemies;

        public EnemyLiveObserver(
            GameSessionData gameSessionData, 
            EnemySpawner enemySpawner, 
            IStatisticsWriter analytics)
        {
            _enemySpawner = enemySpawner;
            _gameSessionData = gameSessionData;
            _activeEnemies = new List<Enemy>();
            _analytics = analytics;

            _enemySpawner.OnSpawned += OnEnemySpawned;
        }

        public void Dispose()
        {
            _enemySpawner.OnSpawned -= OnEnemySpawned;

            if (_activeEnemies.Count == 0)
                return;

            foreach (Enemy enemy in _activeEnemies)
                enemy.OnDied -= OnEnemyDied;
        }

        private void OnEnemySpawned(Enemy enemy)
        {
            enemy.OnDied += OnEnemyDied;
            _activeEnemies.Add(enemy);
        }

        private void OnEnemyDied(Enemy enemy)
        {
            if (enemy.Name == EnemyNames.UFO)
                _analytics.CountUfoDeath();
            else
                _analytics.CountAsteroidDeath();
                

            enemy.OnDied -= OnEnemyDied;
            _activeEnemies.Remove(enemy);

            _gameSessionData.Add(enemy.Reward);

            if (enemy.Name == EnemyNames.AsteroidBig)
                OnBigDied(enemy.transform.position);
            
            OnDied?.Invoke(enemy.transform.position);
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