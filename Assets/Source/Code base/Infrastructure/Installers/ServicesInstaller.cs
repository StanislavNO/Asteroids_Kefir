using Zenject;

namespace Assets.Source.Code_base
{
    public class ServicesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindScoreManager();
            BindFactory();
        }

        private void BindFactory()
        {
            Container.BindInterfacesTo<EnemyFactory>().AsSingle();
            Container.BindInterfacesTo<BulletFactory>().AsSingle();
        }

        private void BindScoreManager()
        {
            Container.BindInterfacesAndSelfTo<ScoreManager>().AsSingle();
        }
    }
}