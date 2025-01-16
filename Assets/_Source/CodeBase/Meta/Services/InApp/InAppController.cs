using Assets._source._code_base.Core.View.UI;
using System;
using Zenject;

namespace Assets._Source.CodeBase.Meta.Services.InApp
{
    public class InAppController : IInitializable, IDisposable
    {
        private readonly MenuDisplay _menuDisplay;
        private readonly IStoreBuyer _storeBuyer;
        private readonly IReadonlyStore _readonlyStore;

        public InAppController(
            MenuDisplay menuDisplay, 
            IStoreBuyer storeBuyer, 
            IReadonlyStore readonlyStore)
        {
            _menuDisplay = menuDisplay;
            _storeBuyer = storeBuyer;
            _readonlyStore = readonlyStore;
        }

        public void Initialize()
        {
            if (_readonlyStore.IsAdsRemoved)
                _menuDisplay.HideRemoveAdsButton();

            _menuDisplay.BuyRemoveAdsButtonClick += OnNoAdsPurchased;
        }

        public void Dispose()
        {
            _menuDisplay.BuyRemoveAdsButtonClick -= OnNoAdsPurchased;
        }

        private void OnNoAdsPurchased()
        {
            _storeBuyer.BuyRemoveAds();
            _menuDisplay.HideRemoveAdsButton();
        }
    }
}