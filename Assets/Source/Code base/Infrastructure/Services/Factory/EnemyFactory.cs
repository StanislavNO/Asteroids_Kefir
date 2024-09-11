using System;

namespace Assets.Source.Code_base
{
    public class EnemyFactory /*: IEnemyFactory*/
    {
        public EnemyFactory()
        {

        }

        //public Enemy Create(EnemyNames name)
        //{
        //    switch (name)
        //    {
        //        case EnemyNames.AsteroidBig:
        //            return CreateAsteroid(_prefabs.EnemyPrefabs.AsteroidBig);

        //        case EnemyNames.AsteroidMini:
        //            return CreateAsteroid(_prefabs.EnemyPrefabs.AsteroidMini);

        //        case EnemyNames.UFO:
        //            return CreateUfo();

        //        default: return null;
        //    }
        //}

        //private Enemy CreateAsteroid(Enemy prefab)
        //{
        //    Enemy enemy = Object.Instantiate(prefab);
        //    enemy.Init(_pauseController);
        //    return enemy;
        //}

        //private Enemy CreateUfo()
        //{
        //    Enemy enemy = Object.Instantiate(_prefabs.EnemyPrefabs.Ufo);
        //    enemy.GetComponent<CharacterFollower>().SetTarget(_character);
        //    enemy.Init(_pauseController);
        //    return enemy;
        //}
    }
}