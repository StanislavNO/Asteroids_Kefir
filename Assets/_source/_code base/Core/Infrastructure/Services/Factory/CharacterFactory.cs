using UnityEngine;
using Zenject;

namespace Assets.Source.Code_base.Infrastructure.Services.Factory
{
    public class CharacterFactory : IFactory<Character>
    {
        private readonly IInitializer _instantiator;
        private readonly Character _prefab;
        private readonly SpawnPointMarker _spawnPoint;

        public CharacterFactory(PrefabsConfig prefabsConfig, SpawnPointMarker playerSpawnPoint, IInitializer instantiator)
        {
            _instantiator = instantiator;
            _prefab = prefabsConfig.Player;
            _spawnPoint = playerSpawnPoint;
        }

        public Character Create()
        {
            Character character = _instantiator
                .InstantiatePrefabForComponent<Character>(
                _prefab,
                _spawnPoint.transform.position,
                Quaternion.identity, null);

            return character;
        }
    }
}