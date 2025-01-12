using Assets._source._code_base.Core.View.UI;
using Assets._source._code_base.Meta.Services.InApp;
using System;
using Zenject;

namespace Assets._source._code_base.Meta.Services
{
    public class InAppController : IInitializable, IDisposable
    {
        private readonly PurchaseSaver _purchaseSaver;
        private readonly MenuDisplay _menuDisplay;

        public InAppController(PurchaseSaver purchaseSaver, MenuDisplay menuDisplay)
        {
            _purchaseSaver = purchaseSaver;
            _menuDisplay = menuDisplay;
        }

        public void Initialize()
        {
            if (_purchaseSaver.AdsStatus == AdsStatus.Deactivate)
                _menuDisplay.HideStoreButton();

            _menuDisplay.NoAdsComplied += OnNoAdsBayed;
        }

        public void Dispose()
        {
            _menuDisplay.NoAdsComplied -= OnNoAdsBayed;
        }

        private void OnNoAdsBayed()
        {
            _purchaseSaver.Save();
        }
    }
}