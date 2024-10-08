using UnityEngine;
using Zenject;

namespace Assets.Source.Code_base.Infrastructure.Services.Factory
{
    public class CharacterFactory : IFactory<Character>
    {
        private readonly Character _prefab;
        private readonly SpawnPointMarker _spawnPoint;
        private readonly CharacterConfig _characterConfig;
        private readonly Weapon _weapon;

        public CharacterFactory(PrefabsConfig prefabsConfig, SpawnPointMarker playerSpawnPoint, CharacterConfig characterConfig, Weapon weapon)
        {
            Debug.Log("Factory ()");
            _prefab = prefabsConfig.Player;
            _spawnPoint = playerSpawnPoint;
            _characterConfig = characterConfig;
            _weapon = weapon;
        }

        public Character Create()
        {
            Character character = GameObject.Instantiate(
                _prefab,
                _spawnPoint.transform.position,
                Quaternion.identity, null);

            character.Initialize(_characterConfig, _weapon);

            return character;
        }
    }
}
