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
            _menuDisplay.OnStartGameButtonClicked += OnOnStartGameClicked;
            _menuDisplay.OnOpenedStorageButtonClicked += OnOnOpenedStorageClicked;
            _menuDisplay.OnClosedStorageButtonClicked += OnOnClosedStorage;
        }

        public void Dispose()
        {
            _menuDisplay.OnStartGameButtonClicked -= OnOnStartGameClicked;
            _menuDisplay.OnOpenedStorageButtonClicked -= OnOnOpenedStorageClicked;
            _menuDisplay.OnClosedStorageButtonClicked -= OnOnClosedStorage;
        }

        private void OnOnStartGameClicked()
        {
            _sceneSwitcher.LoadGameAsync(null, SceneNames.Game);
        }

        private void OnOnClosedStorage()
        {
            _menuDisplay.HideStore();
            _menuDisplay.ShowMenu();
        }

        private void OnOnOpenedStorageClicked()
        {
            _menuDisplay.HideMenu();
            _menuDisplay.ShowStoreWindow();
        }
    }
}
