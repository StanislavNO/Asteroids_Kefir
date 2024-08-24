using System;
using UnityEngine;

namespace Assets.Source.Code_base
{
    [CreateAssetMenu(fileName = "EnemyFactory", menuName = "Configs")]
    public class EnemyFactory : ScriptableObject
    {
        [SerializeField] private Enemy _asteroidMiniPrefab;
        [SerializeField] private Enemy _asteroidBigPrefab;
        [SerializeField] private Enemy _ufoPrefab;

        private Transform _character;

        public void Init(Transform character)
        {
            if (character is null)
                throw new ArgumentNullException(nameof(character));

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