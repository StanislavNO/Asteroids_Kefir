using UnityEngine;

namespace Assets.Source.Code_base
{
    [CreateAssetMenu(fileName = "EnemyFactory", menuName = "EnemyConfigs")]
    public class EnemyFactory : ScriptableObject
    {
        [SerializeField] private readonly Enemy _asteroidMiniPrefab;
        [SerializeField] private readonly Enemy _asteroidBigPrefab;
        [SerializeField] private readonly Enemy _ufoPrefab;

        private Transform _character;

        public void Init(Transform character)
        {
            _character = character;
        }

        public Enemy Create(EnemyNames name)
        {
            switch (name)
            {
                case EnemyNames.AsteroidBig:
                    return Instantiate(_asteroidBigPrefab);

                case EnemyNames.AsteroidMini:
                    return Instantiate(_asteroidMiniPrefab);

                case EnemyNames.UFO:
                    return CreateUfo();

                default: return null;
            }
        }

        private Enemy CreateUfo()
        {
            Enemy enemy = Instantiate(_ufoPrefab);
            enemy.GetComponent<CharacterFollower>().SetTarget(_character);
            return enemy;
        }
    }
}