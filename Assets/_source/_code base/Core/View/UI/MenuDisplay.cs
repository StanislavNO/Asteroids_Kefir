using Assets._source._code_base.Core.Infrastructure.Services.AnimatorService;
using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets._source._code_base.Core.View.UI
{
    public class MenuDisplay : MonoBehaviour
    {
        public event Action StartGameButtonClick;
        public event Action OpenedStorageButtonClick;
        public event Action ClosedStorageButtonClick;
        public event Action BayStorageButtonClick;

        public event Action NoAdsComplied;

        [SerializeField] private GameObject _menuWindow;
        [SerializeField] private GameObject _storageWindow;

        [Space]
        [SerializeField] private Button _startGame;
        [SerializeField] private Button _openStorage;
        [SerializeField] private Button _closeStorage;
        [SerializeField] private Button _bayStorage;

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
            _bayStorage.onClick.AddListener(OnBayStorageButtonClick);
        }

        private void OnDisable()
        {
            _startGame.onClick.RemoveListener(OnStartGameButtonClick);
            _openStorage.onClick.RemoveListener(OnOpenedStorageButtonClick);
            _closeStorage.onClick.RemoveListener(OnCloseStorageButtonClick);
            _bayStorage.onClick.RemoveListener(OnBayStorageButtonClick);
        }

        public void InvokeBayRemoveAds()
        {
            Debug.Log("onn");
            NoAdsComplied?.Invoke();
        }

        public void HideStoreButton() => _bayStorage.interactable = false;

        public void ShowStorage()
        {
            _storageWindow.SetActive(true);
            _animatorService.ShowBounds(_storageWindow.transform, 1);
        }

        public void HideStorage()
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

        private void OnStartGameButtonClick() => StartGameButtonClick?.Invoke();
        private void OnOpenedStorageButtonClick() => OpenedStorageButtonClick?.Invoke();
        private void OnCloseStorageButtonClick() => ClosedStorageButtonClick?.Invoke();
        private void OnBayStorageButtonClick() => BayStorageButtonClick?.Invoke();
    }
}