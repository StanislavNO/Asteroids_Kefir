using System;
using Assets._Source.CodeBase.Core.Infrastructure.Services.AnimatorService;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets._Source.CodeBase.Core.View.UI
{
    public class GameOverDisplay : MonoBehaviour
    {
        public event Action OnRestartButtonClicked;

        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private Canvas _gameOverPanel;
        [SerializeField] private Button _restartButton;
        
        private IAnimatorService _animatorService;

        [Inject]
        private void Construct(IAnimatorService animatorService)
        {
            _animatorService = animatorService;
        }
        
        private void Awake()
        {
            _restartButton.onClick.AddListener(OnRestartButtonClick);
        }

        private void OnDestroy()
        {
            _restartButton.onClick.RemoveListener(OnRestartButtonClick);
        }

        public void Show(int score)
        {
            _gameOverPanel.gameObject.SetActive(true);
            _animatorService.ShowBounds(_gameOverPanel.transform, 1);
            _scoreText.SetText(score.ToString());
        }

        public void Hide() =>
            _gameOverPanel.gameObject.SetActive(false);

        private void OnRestartButtonClick()
        {
            _restartButton.interactable = false;
            OnRestartButtonClicked?.Invoke();
        }
    }
}