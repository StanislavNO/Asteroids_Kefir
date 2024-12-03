using Assets._source._code_base.Meta.Services.Analytics;
using Zenject;

namespace Assets._source._code_base.Meta
{
    internal class MetaServicesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindAnalytics();
        }

        private void BindAnalytics()
        {
            Container
                .BindInterfacesAndSelfTo<PlayerEventCounter>()
                .AsSingle();

            Container
                .BindInterfacesTo<FirebaseAnalyticsController>()
                .AsSingle()
                .NonLazy();
        }
    }
}