﻿using Assets._Source.CodeBase.Core.Controllers;
using Assets._Source.CodeBase.Core.Gameplay.BehaviourEffectors;
using Assets._Source.CodeBase.Meta.Services.InApp;
using Assets._Source.CodeBase.Meta.View;
using System;
using Zenject;

namespace Assets._Source.CodeBase.Meta.Services.Ads
{
    internal class AdsController : IInitializable, IDisposable
    {
        private readonly IAdsLoader _adsLoader;
        private readonly IAdsViewer _adsViewer;

        private readonly ContinueDisplay _continueDisplay;
        private readonly IGameOverController _gameOverController;
        private readonly IReadOnlyCharacter _character;
        private readonly IReadonlyStore _store;

        public AdsController(
            IReadonlyStore store,
            IAdsLoader adsLoader,
            IAdsViewer adsViewer,
            ContinueDisplay continueDisplay,
            IGameOverController gameOverController,
            IReadOnlyCharacter character)
        {
            _store = store;
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

            _character.OnDied += OnCharacterDied;
            _continueDisplay.OnShowButtonClicked += OnShowAdsButtonClicked;
            _continueDisplay.OnCloseButtonClicked += OnCloseDisplayButtonClicked;
        }

        public void Dispose()
        {
            _character.OnDied -= OnCharacterDied;
            _continueDisplay.OnShowButtonClicked -= OnShowAdsButtonClicked;
            _continueDisplay.OnCloseButtonClicked -= OnCloseDisplayButtonClicked;
        }

        private async void OnShowAdsButtonClicked()
        {
            _continueDisplay.Hide();

            if (_store.IsAdsRemoved == false)
            {
                bool isShowComplied = await _adsViewer.ShowReward();

                if (isShowComplied)
                    _gameOverController.Continue();
                else
                    _gameOverController.GameOver();

                return;
            }

            _gameOverController.Continue();
        }

        private async void OnCloseDisplayButtonClicked()
        {
            _continueDisplay.Hide();

            if (_store.IsAdsRemoved == false)
                await _adsViewer.ShowInterstitial();

            _gameOverController.GameOver();
        }

        private void OnCharacterDied()
        {
            if (_gameOverController.ContinueCount > 0)
                _continueDisplay.Show();
            else
                _gameOverController.GameOver();
        }
    }
}