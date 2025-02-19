using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Source.CodeBase.Meta.View
{
    internal class ContinueDisplay : MonoBehaviour
    {
        public event Action OnShowButtonClicked;
        public event Action OnCloseButtonClicked;

        [SerializeField] private Canvas _continuePanel;
        [SerializeField] private Button _showButton;
        [SerializeField] private Button _closeButton;

        private void Awake()
        {
            _showButton.onClick.AddListener(OnShowAdsButtonClick);
            _closeButton.onClick.AddListener(OnCloseButtonClick);
        }

        private void OnDestroy()
        {
            _showButton.onClick.RemoveListener(OnShowAdsButtonClick);
            _closeButton.onClick.RemoveListener(OnCloseButtonClick);
        }

        public void Show()
        {
            _continuePanel.gameObject.SetActive(true);
        }

        public void Hide()
        {
            _continuePanel.gameObject.SetActive(false);
        }

        private void OnShowAdsButtonClick()
        {
            OnShowButtonClicked?.Invoke();
        }

        private void OnCloseButtonClick()
        {
            OnCloseButtonClicked?.Invoke();
        }
    }
}
