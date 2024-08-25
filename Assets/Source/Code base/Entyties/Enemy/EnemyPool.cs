using System.Collections.Generic;
using UnityEngine;

namespace Assets.Source.Code_base
{
    public class EnemyPool
    {
        private readonly Dictionary<EnemyNames, Queue<Enemy>> _enemies;
        private readonly EnemyFactory _enemyFactory;

        public EnemyPool(EnemyFactory enemyFactory)
        {
            _enemyFactory = enemyFactory;

            _enemies = new Dictionary<EnemyNames, Queue<Enemy>>();

            _enemies[EnemyNames.AsteroidBig] = new Queue<Enemy>();
            _enemies[EnemyNames.AsteroidMini] = new Queue<Enemy>();
            _enemies[EnemyNames.UFO] = new Queue<Enemy>();
        }

        public Enemy Get(EnemyNames name)
        {
            Enemy enemy;

            if (_enemies[name].Count > 0)
                enemy = _enemies[name].Dequeue();
            else
                enemy = _enemyFactory.Create(name);

            enemy.gameObject.SetActive(true);

            return enemy;
        }

        public void Put(Enemy enemy)
        {
            enemy.gameObject.SetActive(false);
            _enemies[enemy.Name].Enqueue(enemy);
        }
    }
}