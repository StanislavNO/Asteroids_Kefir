using Assets._source._code_base.Meta.Infrastructure.EntryPoint;
using Assets._source._code_base.Meta.Services.RemoteConfig;
using System;
using Zenject;

namespace Assets._source._code_base.Meta
{
    internal class MetaBootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindSDKInitializers();
            BindBootstrap();
            BindRemoteConfig();
        }

        private void BindRemoteConfig()
        {
            Container
                .BindInterfacesAndSelfTo<RemoteConfigProvider>()
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<ConfigsController>()
                .AsSingle();
        }

        private void BindSDKInitializers()
        {
            Container
                .BindInterfacesAndSelfTo<RemoteConfigInitializer>()
                .AsSingle();

            Container
                .BindInterfacesTo<FirebaseInitializer>()
                .AsSingle();

            Container
                .BindInterfacesTo<UnityAdsInitializer>()
                .AsSingle();
        }

        private void BindBootstrap()
        {
            Container
                .BindInterfacesTo<Bootstrap>()
                .AsSingle()
                .NonLazy();
        }
    }
}