using Assets._source._code_base.Meta.Infrastructure.EntryPoint;
using Zenject;

namespace Assets._source._code_base.Meta
{
    internal class MetaBootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindSDKInitializers();
            BindBootstrap();
        }

        private void BindSDKInitializers()
        {
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