﻿using Assets.Source.Code_base.Infrastructure.Controllers;
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
        [SerializeField] private HeadsUpDisplay _headsUpDisplay;
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
            BindDisplays();
            BindGameManagers();
            BindCharacterControllers();
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
            Container.BindInterfacesAndSelfTo<SceneSwitcher>().AsSingle();
            Container.BindInterfacesTo<GameOverHandler>().AsSingle();
        }

        private void BindCharacterControllers()
        {
            Container.BindInterfacesTo<InputHandler>().AsSingle().NonLazy();
            Container.BindInterfacesTo<ViewController>().AsSingle().NonLazy();
        }
    }
}