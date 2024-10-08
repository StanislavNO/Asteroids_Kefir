using Assets.Source.Code_base.Infrastructure.Services.Factory;
using UnityEngine;
using Zenject;

namespace Assets.Source.Code_base
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private SpawnPointMarker _characterSpawnPoint;
        [SerializeField] private CharacterConfig _characterConfig;
        [SerializeField] private Character _prefab;

        [SerializeField] private GameOverDisplay _gameOverDisplay;
        [SerializeField] private EnemySpawner _enemySpawner;

        public override void InstallBindings()
        {
            BindCharacterConfig();
            BindSpawnPoint();
            BindWeapon();
            BindCharacter();
            BindEnemyFactory();
            BindScore();
            BindSpawner();
            BindEnemyManager();
            BindGameOverDisplay();
            BindGameManagers();
        }

        private void BindSpawnPoint()
        {
            Container.Bind<SpawnPointMarker>()
                .FromInstance(_characterSpawnPoint)
                .AsSingle();
        }

        private void BindSpawner()
        {
            Container.Bind<EnemySpawner>()
                .FromInstance(_enemySpawner)
                .AsSingle();
        }

        private void BindGameManagers()
        {
            Container.BindInterfacesAndSelfTo<SceneSwitcher>().AsSingle();
            Container.BindInterfacesTo<GameOverManager>()
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

        private void BindEnemyFactory()
        {
            Container.BindInterfacesAndSelfTo<EnemyFactory>().AsSingle();
        }

        private void BindWeapon()
        {
            Container.BindInterfacesTo<BulletFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<Weapon>().AsSingle();
        }

        private void BindCharacter()
        {
            Container.BindInterfacesTo<Character>()
                .FromFactory<Character, CharacterFactory>()
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