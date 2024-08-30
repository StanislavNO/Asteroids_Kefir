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

        private IReadOnlyScore _score;

        public void Init(IReadOnlyScore scoreManager)
        {
            _score = scoreManager;
            _restartButton.onClick.AddListener(RestartButtonOnClick);
        }

        public void ShowScore() =>
            _scoreText.SetText(_score.Score.ToString());

        public void ShowGameOverPanel() =>
            _gameOverPanel.gameObject.SetActive(true);

        public void HideGameOverPanel() =>
            _gameOverPanel.gameObject.SetActive(false);

        private void RestartButtonOnClick()
        {
            _restartButton.interactable = false;
            RestartButtonClicked?.Invoke();
        }
    }
}