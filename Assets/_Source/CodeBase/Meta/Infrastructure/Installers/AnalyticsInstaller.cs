using Assets._Source.CodeBase.Meta.Services.Analytics;
using Assets._Source.CodeBase.Meta.Services.Analytics.SoftDevKits;
using Zenject;

namespace Assets._Source.CodeBase.Meta.Infrastructure.Installers
{
    public class AnalyticsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
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
    }
}