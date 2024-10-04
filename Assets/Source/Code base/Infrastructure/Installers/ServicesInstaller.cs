using UnityEngine;
using Zenject;

namespace Assets.Source.Code_base
{
    public class ServicesInstaller : MonoInstaller
    {
        [SerializeField] private GameOverDisplay _gameOverDisplay;
        [SerializeField] private EnemySpawner _enemySpawner;

        public override void InstallBindings()
        {
            BindScore();
            BindSpawner();
            BindEnemyManager();
            BindGameOverDisplay();
            BindGameOverDisplay();
            BindGameManagers();
            BindFactory();
        }

        private void BindSpawner()
        {
            Container.Bind<EnemySpawner>().FromInstance(_enemySpawner).AsSingle();
        }

        private void BindGameManagers()
        {
            Container.BindInterfacesAndSelfTo<GameSceneManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameOverManager>().AsSingle();
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
    }
}