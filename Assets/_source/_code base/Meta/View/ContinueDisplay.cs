using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._source._code_base.Meta.View
{
    internal class ContinueDisplay : MonoBehaviour
    {
        public event Action ShowButtonClicked;
        public event Action CloseButtonClicked;

        [SerializeField] private Canvas _continuePanel;
        [SerializeField] private Button _showButton;
        [SerializeField] private Button _closeButton;

        private void OnEnable()
        {
            _showButton.onClick.AddListener(OnShowAdsButtonClick);
            _closeButton.onClick.AddListener(OnCloseButtonClick);
        }

        private void OnDisable()
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
            ShowButtonClicked?.Invoke();
        }

        private void OnCloseButtonClick()
        {
            CloseButtonClicked?.Invoke();
        }
    }
}
