using Assets._source._code_base.Core.Controllers;
using Assets._source._code_base.Core.View.UI;
using System;
using UnityEngine;
using Zenject;

namespace Assets._source._code_base.Core.Infrastructure.Installers
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