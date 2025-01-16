using Assets._Source.CodeBase.Core.Common;
using Assets._Source.CodeBase.Core.Controllers;
using Assets._Source.CodeBase.Core.Gameplay.BehaviourEffectors;
using Assets._Source.CodeBase.Core.Gameplay.Enemies;
using Assets._Source.CodeBase.Core.Infrastructure.Services.Factory;
using Assets._Source.CodeBase.Core.Infrastructure.Services.Score;
using Assets._Source.CodeBase.Core.View.UI;
using UnityEngine;
using Zenject;

namespace Assets._Source.CodeBase.Core.Infrastructure.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private SpawnPointMarker _characterSpawnPoint;

        [SerializeField] private GameOverDisplay _gameOverDisplay;
        [SerializeField] private HeadsUpDisplay _headsUpDisplay;
        [SerializeField] private EnemySpawner _enemySpawner;
        [SerializeField] private WeaponAudioController _audioController;

        public override void InstallBindings()
        {
            BindAudioController();
            BindSpawnPoint();
            BindWeapon();
            BindCharacter();
            BindEnemyFactory();
            BindScore();
            BindSpawner();
            BindEnemyManager();
            BindDisplays();
            BindGameManagers();
            BindCharacterControllers();
        }

        private void BindAudioController()
        {
            Container.Bind<WeaponAudioController>()
                .FromInstance(_audioController)
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
            Container.BindInterfacesTo<Weapon>().AsSingle();
        }

        private void BindCharacter()
        {
            Container.BindInterfacesTo<Character>()
                .FromFactory<Character, CharacterFactory>()
                .AsSingle();

            Container.Bind<Mover>().AsSingle();
            Container.Bind<Rotator>().AsSingle();
        }

        private void BindEnemyFactory()
        {
            Container.BindInterfacesTo<EnemyFactory>().AsSingle();
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
            Container.BindInterfacesTo<EnemyManager>().AsSingle();
        }

        private void BindDisplays()
        {
            Container.Bind<GameOverDisplay>()
                .FromInstance(_gameOverDisplay)
                .AsSingle();

            Container.BindInterfacesTo<HeadsUpDisplay>()
                .FromInstance(_headsUpDisplay)
                .AsSingle();
        }

        private void BindGameManagers()
        {
            Container.BindInterfacesTo<GameOverController>().AsSingle();
        }

        private void BindCharacterControllers()
        {
            Container.BindInterfacesTo<PlayerInputController>().AsSingle().NonLazy();
            Container.BindInterfacesTo<GameViewController>().AsSingle().NonLazy();
        }
    }
}