using Assets._Source.CodeBase.Meta.Services.Analytics;
using Assets._Source.CodeBase.Meta.Services.Analytics.SoftDevKits;
using Assets._Source.CodeBase.Meta.Services.InApp;
using Zenject;

namespace Assets._Source.CodeBase.Meta.Infrastructure.Installers
{
    internal class MetaGlobalInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindInAppService();
            BindAnalytics();
        }

        private void BindAnalytics()
        {
            Container
                .Bind<IAnalyticProvider>()
                .To<FirebaseAnalytic>()
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<EventCounter>()
                .AsSingle();

            Container
                .BindInterfacesTo<AnalyticsController>()
                .AsSingle()
                .NonLazy();
        }

        private void BindInAppService()
        {
            Container.BindInterfacesTo<InAppService>().AsSingle();
        }
    }
}