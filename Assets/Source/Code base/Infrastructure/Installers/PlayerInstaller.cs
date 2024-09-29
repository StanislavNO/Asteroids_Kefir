using System;
using UnityEngine;
using Zenject;


namespace Assets.Source.Code_base
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private Transform _characterSpawnPoint;
        [SerializeField] private CharacterConfig _characterConfig;
        [SerializeField] private Character _prefab;

        public override void InstallBindings()
        {
            BindConfig();
            BindWeapon();
            BindInstantiateCharacter();
        }

        private void BindWeapon()
        {
            Container.BindInterfacesAndSelfTo<Weapon>().AsSingle();
        }

        private void BindInstantiateCharacter()
        {
            Character character = Container.InstantiatePrefabForComponent<Character>
                (_prefab, _characterSpawnPoint.position, Quaternion.identity, null);

            Container.BindInterfacesAndSelfTo<Character>()
                .FromInstance(character)
                .AsSingle();
        }

        private void BindConfig()
        {
            Container.Bind<CharacterConfig>().FromInstance(_characterConfig).AsSingle();
        }
    }
}