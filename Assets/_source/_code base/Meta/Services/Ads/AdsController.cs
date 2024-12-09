using Assets._source._code_base.Core.Controllers;
using Assets._source._code_base.Meta.Services.Ads.SoftDevKits;
using Assets._source._code_base.Meta.View;
using Assets.Source.Code_base;
using System;
using Zenject;

namespace Assets._source._code_base.Meta.Services.Ads
{
    internal class AdsController : IInitializable, IDisposable
    {
        private readonly IAdsLoader _adsLoader;
        private readonly IAdsViewer _adsViewer;

        private readonly ContinueDisplay _continueDisplay;
        private readonly IGameOverController _gameOverController;
        private readonly IReadOnlyCharacter _character;

        public AdsController(
            IAdsLoader adsLoader,
            IAdsViewer adsViewer,
            ContinueDisplay continueDisplay,
            IGameOverController gameOverController,
            IReadOnlyCharacter character)
        {
            _adsLoader = adsLoader;
            _adsViewer = adsViewer;
            _continueDisplay = continueDisplay;
            _gameOverController = gameOverController;
            _character = character;
        }

        public void Initialize()
        {
            _adsLoader.LoadInterstitial();
            _adsLoader.LoadRewarded();

            _character.Die += OnCharacterDie;
            _continueDisplay.ShowButtonClicked += ShowAdsButtonClicked;
            _continueDisplay.CloseButtonClicked += CloseDisplayButtonClicked;
        }

        public void Dispose()
        {
            _character.Die -= OnCharacterDie;
            _continueDisplay.ShowButtonClicked -= ShowAdsButtonClicked;
            _continueDisplay.CloseButtonClicked -= CloseDisplayButtonClicked;
        }

        private async void ShowAdsButtonClicked()
        {
            if (_gameOverController.ContinueCount <= 0)
            {
                bool r = await _adsViewer.ShowReward();
            }
            else
                _gameOverController.GameOver();
        }

        private void CloseDisplayButtonClicked()
        {
            _adsViewer.ShowInterstitial();
        }

        private void OnCharacterDie()
        {
            _continueDisplay.Show();
        }
    }
}