using System;
using System.Collections;
using UnityEngine;

namespace Assets.Source.Code_base
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private Enemy _asteroidMiniPrefab;
        [SerializeField] private Enemy _asteroidBigPrefab;
        [SerializeField] private CharacterFollower _ufoPrefab;

        private EnemyPool _enemyPool;

        public void SpawnEnemy(EnemyNames name, Vector3 spawnPosition)
        {
            Enemy enemy = _enemyPool.Get(name);
            
        }
    }
}