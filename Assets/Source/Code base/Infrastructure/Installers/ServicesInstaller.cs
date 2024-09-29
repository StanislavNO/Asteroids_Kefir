using System;
using Zenject;

namespace Assets.Source.Code_base
{
    public class ServicesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindScoreManager();
        }

        private void BindScoreManager()
        {
            Container.BindInterfacesAndSelfTo<ScoreManager>().AsSingle();
        }
    }
}