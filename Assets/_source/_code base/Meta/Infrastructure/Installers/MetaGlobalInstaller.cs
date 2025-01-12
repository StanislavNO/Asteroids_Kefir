using Assets._source._code_base.Meta.Services.InApp;
using Zenject;

namespace Assets._source._code_base.Meta.Infrastructure.Installers
{
    internal class MetaGlobalInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindPurchaseSaver();
        }

        private void BindPurchaseSaver()
        {
            Container.BindInterfacesAndSelfTo<PurchaseSaver>().AsSingle();
        }
    }
}