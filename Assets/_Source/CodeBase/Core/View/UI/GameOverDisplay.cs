using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Source.CodeBase.Core.View.UI
{
    public class GameOverDisplay : MonoBehaviour
    {
        public event Action RestartButtonClicked;

        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private Canvas _gameOverPanel;
        [SerializeField] private Button _restartButton;

        private void OnEnable()
        {
            _restartButton.onClick.AddListener(OnRestartButtonClick);
        }

        private void OnDisable()
        {
            _restartButton.onClick.RemoveListener(OnRestartButtonClick);
        }

        public void Show(int score)
        {
            _gameOverPanel.gameObject.SetActive(true);
            _scoreText.SetText(score.ToString());
        }

        public void Hide() =>
            _gameOverPanel.gameObject.SetActive(false);

        private void OnRestartButtonClick()
        {
            _restartButton.interactable = false;
            RestartButtonClicked?.Invoke();
        }
    }
}