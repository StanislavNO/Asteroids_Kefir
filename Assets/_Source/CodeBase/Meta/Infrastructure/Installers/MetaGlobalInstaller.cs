using Assets._Source.CodeBase.Meta.Services.InApp;
using Zenject;

namespace Assets._Source.CodeBase.Meta.Infrastructure.Installers
{
    internal class MetaGlobalInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindInAppService();
        }

        private void BindInAppService()
        {
            Container.BindInterfacesTo<InAppService>().AsSingle();
        }
    }
}