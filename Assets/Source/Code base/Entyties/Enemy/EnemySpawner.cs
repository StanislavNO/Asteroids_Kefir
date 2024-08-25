using System;
using UnityEngine;

namespace Assets.Source.Code_base
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private EnemyFactory _factory;

        private EnemyPool _enemyPool;
        private EnemyLifeHandler _lifeController;

        public void Init(Transform character, EnemyLifeHandler enemyLifeController)
        {
            CheckForNull(character, enemyLifeController);

            _factory.Init(character);
            _enemyPool = new(_factory);
            _lifeController = enemyLifeController;
            _lifeController.Initialize(_enemyPool, this);
        }

        public void SpawnEnemy(EnemyNames name, Vector3 spawnPosition)
        {
            Enemy enemy = _enemyPool?.Get(name);
            _lifeController.AddEnemy(enemy);
        }

        private static void CheckForNull(Transform character, EnemyLifeHandler enemyLifeController)
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