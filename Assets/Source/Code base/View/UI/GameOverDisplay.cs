using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Code_base
{
    public class GameOverDisplay : MonoBehaviour
    {
        public event Action RestartButtonClicked;

        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private Canvas _gameOverPanel;
        [SerializeField] private Button _restartButton;

        private void Awake()
        {
            _restartButton.onClick.AddListener(OnRestartButtonClick);
        }

        private void OnDestroy()
        {
            _restartButton.onClick.RemoveListener(OnRestartButtonClick);
        }

        public void ShowScore(int value) =>
            _scoreText.SetText(value.ToString());

        public void ShowGameOverPanel() =>
            _gameOverPanel.gameObject.SetActive(true);

        public void HideGameOverPanel() =>
            _gameOverPanel.gameObject.SetActive(false);

        private void OnRestartButtonClick()
        {
            _restartButton.interactable = false;
            RestartButtonClicked?.Invoke();
        }
    }
}