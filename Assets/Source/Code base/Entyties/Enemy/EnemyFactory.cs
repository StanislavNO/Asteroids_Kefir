using UnityEngine;

namespace Assets.Source.Code_base
{
    public class EnemyFactory : IFactory<Enemy>
    {
        private readonly Transform _character;

        private readonly Enemy _asteroidMiniPrefab;
        private readonly Enemy _asteroidBigPrefab;
        private readonly Enemy _ufoPrefab;

        public EnemyFactory(Transform character, PrefabsConfig enemyConfig)
        {
            _character = character;
            _asteroidMiniPrefab = enemyConfig.EnemyPrefabs.AsteroidMini;
            _asteroidBigPrefab = enemyConfig.EnemyPrefabs.AsteroidBig;
            _ufoPrefab = enemyConfig.EnemyPrefabs.Ufo;
        }

        public Enemy Create(EnemyNames name)
        {
            switch (name)
            {
                case EnemyNames.AsteroidBig:
                    return Object.Instantiate(_asteroidBigPrefab);

                case EnemyNames.AsteroidMini:
                    return Object.Instantiate(_asteroidMiniPrefab);

                case EnemyNames.UFO:
                    return CreateUfo();

                default: return null;
            }
        }

        public Enemy Create()
        {
            throw new System.NotImplementedException();
        }

        private Enemy CreateUfo()
        {
            Enemy enemy = Object.Instantiate(_ufoPrefab);
            enemy.GetComponent<CharacterFollower>().SetTarget(_character);
            return enemy;
        }
    }
}