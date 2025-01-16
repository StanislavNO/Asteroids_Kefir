using Assets._Source.CodeBase.Core.Controllers;
using Assets._Source.CodeBase.Core.View.UI;
using UnityEngine;
using Zenject;

namespace Assets._Source.CodeBase.Core.Infrastructure.Installers
{
    internal class MenuInstaller : MonoInstaller
    {
        [SerializeField] private MenuDisplay _menuDisplay;

        public override void InstallBindings()
        {
            BindMenuDisplay();
            BindControllers();
        }

        private void BindControllers()
        {
            Container
                .BindInterfacesTo<MenuViewController>()
                .AsSingle()
                .NonLazy();
        }

        private void BindMenuDisplay()
        {
            Container.Bind<MenuDisplay>()
                .FromInstance(_menuDisplay)
                .AsSingle();
        }
    }
}