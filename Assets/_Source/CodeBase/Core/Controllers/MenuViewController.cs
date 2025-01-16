using Assets._Source.CodeBase.Core.Infrastructure.Services.SceneSwitcher;
using Assets._Source.CodeBase.Core.View.UI;
using System;
using Zenject;

namespace Assets._Source.CodeBase.Core.Controllers
{
    internal class MenuViewController : IInitializable, IDisposable
    {
        private readonly SceneSwitcher _sceneSwitcher;
        private readonly MenuDisplay _menuDisplay;

        public MenuViewController(SceneSwitcher sceneSwitcher, MenuDisplay menuDisplay)
        {
            _sceneSwitcher = sceneSwitcher;
            _menuDisplay = menuDisplay;
        }

        public void Initialize()
        {
            _menuDisplay.StartGameButtonClick += OnStartGameClick;
            _menuDisplay.OpenedStorageButtonClick += OnOpenedStorageClick;
            _menuDisplay.ClosedStorageButtonClick += OnClosedStorage;
        }

        public void Dispose()
        {
            _menuDisplay.StartGameButtonClick -= OnStartGameClick;
            _menuDisplay.OpenedStorageButtonClick -= OnOpenedStorageClick;
            _menuDisplay.ClosedStorageButtonClick -= OnClosedStorage;
        }

        private void OnStartGameClick()
        {
            _sceneSwitcher.LoadGameAsync(null, SceneNames.Game);
        }

        private void OnClosedStorage()
        {
            _menuDisplay.HideStore();
            _menuDisplay.ShowMenu();
        }

        private void OnOpenedStorageClick()
        {
            _menuDisplay.HideMenu();
            _menuDisplay.ShowStoreWindow();
        }
    }
}
