using System;
using Zenject;

namespace Assets.Source.Code_base
{
    public class ServicesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindScoreManager();
            BindPauseController();
            BindFactory();
        }

        private void BindFactory()
        {
            Container.BindInterfacesAndSelfTo<EnemyFactory>().AsSingle();
        }

        private void BindPauseController()
        {
            Container.BindInterfacesAndSelfTo<PauseController>().AsSingle();
        }

        private void BindScoreManager()
        {
            Container.BindInterfacesAndSelfTo<ScoreManager>().AsSingle();
        }
    }
}