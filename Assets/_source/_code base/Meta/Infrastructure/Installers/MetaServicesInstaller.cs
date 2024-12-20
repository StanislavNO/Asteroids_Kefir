﻿using Assets._source._code_base.Meta.Services.Ads;
using Assets._source._code_base.Meta.Services.Ads.SDKs;
using Assets._source._code_base.Meta.Services.Ads.SoftDevKits;
using Assets._source._code_base.Meta.Services.Analytics;
using Assets._source._code_base.Meta.Services.Analytics.SDKs;
using Assets._source._code_base.Meta.View;
using UnityEngine;
using Zenject;

namespace Assets._source._code_base.Meta
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