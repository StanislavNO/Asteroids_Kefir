using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Source.Code_base
{
    public class Factory : IFactory
    {
        private readonly WeaponConfig _weaponStat;
        private readonly AttackPoint _attackPoint;
        private readonly PrefabsConfig _prefabs;
        private readonly Transform _character;

        public Factory(WeaponConfig weaponStat, AttackPoint attackPoint, PrefabsConfig prefabsConfig, Transform character)
        {
            _weaponStat = weaponStat;
            _attackPoint = attackPoint;
            _prefabs = prefabsConfig;
            _character = character;
        }

        public Bullet Create()
        {
            Bullet bullet = Object.Instantiate(
                _prefabs.DefaultBulletPrefab,
                _attackPoint.Position,
                _attackPoint.Rotation);

            return bullet;
        }

        public Enemy Create(EnemyNames name)
        {
            switch (name)
            {
                case EnemyNames.AsteroidBig:
                    return Object.Instantiate(_prefabs.EnemyPrefabs.AsteroidBig);

                case EnemyNames.AsteroidMini:
                    return Object.Instantiate(_prefabs.EnemyPrefabs.AsteroidBig);

                case EnemyNames.UFO:
                    return CreateUfo();

                default: return null;
            }
        }
        private Enemy CreateUfo()
        {
            Enemy enemy = Object.Instantiate(_prefabs.EnemyPrefabs.Ufo);
            enemy.GetComponent<CharacterFollower>().SetTarget(_character);
            return enemy;
        }
    }
}
