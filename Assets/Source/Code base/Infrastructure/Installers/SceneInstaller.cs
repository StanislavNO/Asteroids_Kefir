using Assets.Source.Code_base.Infrastructure.Controllers;
using Assets.Source.Code_base.Infrastructure.Services.Factory;
using UnityEngine;
using Zenject;

namespace Assets.Source.Code_base
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private SpawnPointMarker _characterSpawnPoint;
        [SerializeField] private CharacterConfig _characterConfig;

        [SerializeField] private GameOverDisplay _gameOverDisplay;
        [SerializeField] private EnemySpawner _enemySpawner;
        [SerializeField] private AudioController _audioController;

        public override void InstallBindings()
        {
            BindAudioController();
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
            BindCharacterController();
        }

        private void BindAudioController()
        {
            Container.Bind<AudioController>()
                .FromInstance(_audioController)
                .AsSingle();
        }

        private void BindCharacterConfig()
        {
            Container.Bind<CharacterConfig>()
                .FromInstance(_characterConfig)
                .AsSingle();
        }

        private void BindSpawnPoint()
        {
            Container.Bind<SpawnPointMarker>()
                .FromInstance(_characterSpawnPoint)
                .AsSingle();
        }

        private void BindWeapon()
        {
            Container.BindInterfacesTo<BulletFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<Weapon>().AsSingle();
        }

        private void BindCharacter()
        {
            Container.BindInterfacesAndSelfTo<Character>()
                .FromFactory<Character, CharacterFactory>()
                .AsSingle();

            Container.BindInterfacesAndSelfTo<Mover>().AsSingle();
            Container.BindInterfacesAndSelfTo<Rotator>().AsSingle();
        }

        private void BindEnemyFactory()
        {
            Container.BindInterfacesAndSelfTo<EnemyFactory>().AsSingle();
        }

        private void BindScore()
        {
            Container.BindInterfacesAndSelfTo<ScoreManager>().AsSingle();
        }

        private void BindSpawner()
        {
            Container.Bind<EnemySpawner>()
                .FromInstance(_enemySpawner)
                .AsSingle();
        }

        private void BindEnemyManager()
        {
            Container.BindInterfacesAndSelfTo<EnemyManager>().AsSingle();
        }

        private void BindGameOverDisplay()
        {
            Container.Bind<GameOverDisplay>().FromInstance(_gameOverDisplay);
        }

        private void BindGameManagers()
        {
            Container.BindInterfacesAndSelfTo<SceneSwitcher>().AsSingle();
            Container.BindInterfacesTo<GameOverHandler>()
                .AsSingle()
                .NonLazy();
        }

        private void BindCharacterController()
        {
            Container.BindInterfacesTo<InputHandler>()
                .AsSingle();
        }
    }
}