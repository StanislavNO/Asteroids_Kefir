using Assets._Source.CodeBase.Meta.Services.InApp;
using Zenject;

namespace Assets._Source.CodeBase.Meta.Infrastructure.Installers
{
    internal class MetaMenuInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindInAppController();
        }

        private void BindInAppController()
        {
            Container
                .BindInterfacesTo<InAppController>()
                .AsSingle()
                .NonLazy();
        }
    }
}
