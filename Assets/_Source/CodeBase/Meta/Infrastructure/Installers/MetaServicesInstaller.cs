using Assets._Source.CodeBase.Meta.Services.Ads;
using Assets._Source.CodeBase.Meta.Services.Ads.SoftDevKits;
using Assets._Source.CodeBase.Meta.Services.Analytics;
using Assets._Source.CodeBase.Meta.Services.Analytics.SoftDevKits;
using Assets._Source.CodeBase.Meta.View;
using UnityEngine;
using Zenject;

namespace Assets._Source.CodeBase.Meta.Infrastructure.Installers
{
    internal class MetaServicesInstaller : MonoInstaller
    {
        [SerializeField] private ContinueDisplay _continueDisplay;

        public override void InstallBindings()
        {
            BindAnalytics();
            BindAds();
        }

        private void BindAds()
        {
            Container.Bind<ContinueDisplay>()
                .FromInstance(_continueDisplay)
                .AsSingle();

            Container
                .BindInterfacesTo<UnityAdsViewer>()
                .AsSingle();

            Container
                .BindInterfacesTo<UnityAdsLoader>()
                .AsSingle();

            Container
                .BindInterfacesTo<AdsController>()
                .AsSingle()
                .NonLazy();
        }

        private void BindAnalytics()
        {
            Container
                .Bind<IAnalyticProvider>()
                .To<FirebaseAnalytic>()
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<PlayerEventCounter>()
                .AsSingle();

            Container
                .BindInterfacesTo<AnalyticsController>()
                .AsSingle()
                .NonLazy();
        }
    }
}