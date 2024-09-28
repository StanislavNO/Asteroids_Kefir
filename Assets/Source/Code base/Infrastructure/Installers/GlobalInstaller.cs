using System;
using Zenject;


namespace Assets.Source.Code_base
{
    public class GlobalInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindInput();
        }

        private void BindInput()
        {
            Container.BindInterfacesTo<StandaloneInput>().AsSingle();
        }
    }
}