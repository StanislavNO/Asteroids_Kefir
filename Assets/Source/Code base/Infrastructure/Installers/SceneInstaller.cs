using UnityEngine;
using Zenject;

namespace Assets.Source.Code_base
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private Transform _characterSpawnPoint;
        [SerializeField] private CharacterConfig _characterConfig;
        [SerializeField] private Character _prefab;

        [SerializeField] private GameOverDisplay _gameOverDisplay;
        [SerializeField] private EnemySpawner _enemySpawner;

        public override void InstallBindings()
        {
            BindCharacterConfig();
            BindFactory();
            BindWeapon();
            BindInstantiateCharacter();
            BindScore();
            BindSpawner();
            BindEnemyManager();
            BindGameOverDisplay();
            BindGameManagers();
        }

        private void BindSpawner()
        {
            Container.Bind<EnemySpawner>()
                .FromInstance(_enemySpawner)
                .AsSingle();
        }

        private void BindGameManagers()
        {
            Container.BindInterfacesAndSelfTo<GameSceneManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameOverManager>()
                .AsSingle()
                .NonLazy();
        }

        private void BindScore()
        {
            Container.BindInterfacesAndSelfTo<ScoreManager>().AsSingle();
        }

        private void BindEnemyManager()
        {
            Container.BindInterfacesAndSelfTo<EnemyManager>().AsSingle();
        }

        private void BindGameOverDisplay()
        {
            Container.Bind<GameOverDisplay>().FromInstance(_gameOverDisplay);
        }

        private void BindFactory()
        {
            Container.BindInterfacesTo<EnemyFactory>().AsSingle();
            Container.BindInterfacesTo<BulletFactory>().AsSingle();
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

        private void BindCharacterConfig()
        {
            Container.Bind<CharacterConfig>()
                .FromInstance(_characterConfig)
                .AsSingle();
        }
    }
}