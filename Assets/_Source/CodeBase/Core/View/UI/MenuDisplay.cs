using Assets._Source.CodeBase.Core.Infrastructure.Services.AnimatorService;
using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets._Source.CodeBase.Core.View.UI
{
    public class MenuDisplay : MonoBehaviour
    {
        public event Action OnStartGameButtonClicked;
        public event Action OnOpenedStorageButtonClicked;
        public event Action OnClosedStorageButtonClicked;
        public event Action OnBuyRemoveAdsButtonClicked;

        [SerializeField] private GameObject _menuWindow;
        [SerializeField] private GameObject _storageWindow;

        [Space]
        [SerializeField] private Button _startGame;
        [SerializeField] private Button _openStorage;
        [SerializeField] private Button _closeStorage;
        [SerializeField] private Button _removeAds;

        private IAnimatorService _animatorService;

        [Inject]
        private void Construct(IAnimatorService animatorService)
        {
            _animatorService = animatorService;
        }

        private void OnEnable()
        {
            _startGame.onClick.AddListener(OnStartGameButtonClick);
            _openStorage.onClick.AddListener(OnOpenedStorageButtonClick);
            _closeStorage.onClick.AddListener(OnCloseStorageButtonClick);
            _removeAds.onClick.AddListener(OnBayStorageButtonClick);
        }

        private void OnDisable()
        {
            _startGame.onClick.RemoveListener(OnStartGameButtonClick);
            _openStorage.onClick.RemoveListener(OnOpenedStorageButtonClick);
            _closeStorage.onClick.RemoveListener(OnCloseStorageButtonClick);
            _removeAds.onClick.RemoveListener(OnBayStorageButtonClick);
        }

        public void HideRemoveAdsButton() => _removeAds.interactable = false;

        public void ShowStoreWindow()
        {
            _storageWindow.SetActive(true);
            _animatorService.ShowBounds(_storageWindow.transform, 1);
        }

        public void HideStore()
        {
            _storageWindow.SetActive(false);
        }

        public void ShowMenu()
        {
            _menuWindow.SetActive(true);
        }

        public void HideMenu()
        {
            _menuWindow.SetActive(false);
        }

        private void OnStartGameButtonClick() => OnStartGameButtonClicked?.Invoke();
        private void OnOpenedStorageButtonClick() => OnOpenedStorageButtonClicked?.Invoke();
        private void OnCloseStorageButtonClick() => OnClosedStorageButtonClicked?.Invoke();
        private void OnBayStorageButtonClick() => OnBuyRemoveAdsButtonClicked?.Invoke();
    }
}