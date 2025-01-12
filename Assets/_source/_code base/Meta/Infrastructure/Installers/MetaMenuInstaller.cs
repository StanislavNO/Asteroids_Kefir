using Assets._source._code_base.Meta.Services;
using Zenject;

namespace Assets._source._code_base.Meta.Infrastructure.Installers
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
