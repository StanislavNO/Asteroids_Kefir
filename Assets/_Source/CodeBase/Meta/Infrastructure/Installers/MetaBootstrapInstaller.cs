using Assets._Source.CodeBase.Meta.Infrastructure.EntryPoint;
using Assets._Source.CodeBase.Meta.Infrastructure.EntryPoint.Initializers;
using Assets._Source.CodeBase.Meta.Services.JsonManager;
using Assets._Source.CodeBase.Meta.Services.RemoteConfig;
using Zenject;

namespace Assets._Source.CodeBase.Meta.Infrastructure.Installers
{
    internal class MetaBootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindJsonConvector();
            BindSDKInitializers();
            BindBootstrap();
            BindRemoteConfig();
        }

        private void BindJsonConvector()
        {
            Container
                .BindInterfacesTo<JsonConvector>()
                .AsSingle();
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