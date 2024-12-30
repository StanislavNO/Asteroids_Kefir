using Assets._source._code_base.Core.View.UI;
using Assets.Source.Code_base;
using System;
using Zenject;

namespace Assets._source._code_base.Core.Controllers
{
    internal class MenuViewController : IInitializable, IDisposable
    {
        private readonly SceneSwitcher _sceneSwitcher;
        private readonly MenuDisplay _menuDisplay;
        private readonly LoadingScreen _loadingScreen;

        public MenuViewController(SceneSwitcher sceneSwitcher, MenuDisplay menuDisplay)
        {
            _sceneSwitcher = sceneSwitcher;
            _menuDisplay = menuDisplay;
        }

        public void Initialize()
        {
            _menuDisplay.StartGameButtonClick += OnStartGameClick;
            _menuDisplay.OpenedStorageButtonClick += OnOpenedStorageClick;
            _menuDisplay.BayStorageButtonClick += OnBayAds;
            _menuDisplay.ClosedStorageButtonClick += OnClosedStorage;
        }

        public void Dispose()
        {
            _menuDisplay.StartGameButtonClick -= OnStartGameClick;
            _menuDisplay.OpenedStorageButtonClick -= OnOpenedStorageClick;
            _menuDisplay.BayStorageButtonClick -= OnBayAds;
            _menuDisplay.ClosedStorageButtonClick -= OnClosedStorage;
        }

        private void OnStartGameClick()
        {
            _sceneSwitcher.LoadGameAsync(null, SceneNames.Game);
        }

        private void OnClosedStorage()
        {
            _menuDisplay.HideStorage();
            _menuDisplay.ShowMenu();
        }

        private void OnBayAds()
        {

        }

        private void OnOpenedStorageClick()
        {
            _menuDisplay.HideMenu();
            _menuDisplay.ShowStorage();
        }
    }
}
