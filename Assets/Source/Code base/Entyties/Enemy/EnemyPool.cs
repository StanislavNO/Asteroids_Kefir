using System;
using System.Collections.Generic;

namespace Assets.Source.Code_base
{
    public class EnemyPool
    {
        private readonly Dictionary<EnemyNames, Queue<Enemy>> _enemies;
        private readonly EnemyFactory _enemyFactory;

        public EnemyPool(EnemyFactory enemyFactory)
        {
            _enemyFactory = enemyFactory;
        }

        public Enemy Get(EnemyNames name)
        {
            //if (_enemies.Keys(name).Count > 0)
            //{

            //}
            return null;
        }

        public void Put(Enemy enemy)
        {

        }
    }
}