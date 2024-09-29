using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Source.Code_base
{
    public class GameOverDisplay : MonoBehaviour
    {
        public event Action RestartButtonClicked;

        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private Canvas _gameOverPanel;
        [SerializeField] private Button _restartButton;

        private IReadOnlyScore _score;

        [Inject]
        private void Construct(IReadOnlyScore scoreManager)
        {
            _score = scoreManager;
        }

        private void Awake()
        {
            _restartButton.onClick.AddListener(RestartButtonOnClick);
        }

        private void OnDestroy()
        {
            _restartButton.onClick.RemoveListener(RestartButtonOnClick);
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