using System;
using System.Collections;
using UnityEngine;

namespace Assets.Source.Code_base
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private EnemyFactory _factory;

        private EnemyPool _enemyPool;

        public void Init(Transform character)
        {
            _factory.Init(character);
            _enemyPool = new(_factory);
        }

        public void SpawnEnemy(EnemyNames name, Vector3 spawnPosition)
        {
            Enemy enemy = _enemyPool?.Get(name);
            
        }
    }
}