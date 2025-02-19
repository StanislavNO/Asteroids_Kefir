using Assets._Source.CodeBase.Core.Gameplay.BehaviourEffectors;
using Assets._Source.CodeBase.Core.Infrastructure.Services.SceneSwitcher;
using Assets._Source.CodeBase.Core.Infrastructure.Services.Score;
using Assets._Source.CodeBase.Core.Infrastructure.Services.TimeManager;
using Assets._Source.CodeBase.Core.View.UI;
using System;
using Assets._Source.CodeBase.Meta.Services.Analytics;

namespace Assets._Source.CodeBase.Core.Controllers
{
    internal class GameOverController : IDisposable, IGameOverController
    {
        private readonly IReadOnlyScore _score;
        private readonly IReadOnlyCharacter _character;
        private readonly IEventWriter _analytics;
        private readonly PauseController _pauseController;
        private readonly SceneSwitcher _sceneSwitcher;
        private readonly GameOverDisplay _gameOverDisplay;

        public int ContinueCount { get; private set; } = 1;

        public GameOverController(
            IReadOnlyCharacter character,
            IReadOnlyScore score,
            PauseController pauseController,
            SceneSwitcher sceneSwitcher,
            GameOverDisplay gameOverDisplay,
            IEventWriter analytics)
        {
            _character = character;
            _score = score;
            _pauseController = pauseController;
            _sceneSwitcher = sceneSwitcher;
            _gameOverDisplay = gameOverDisplay;
            _analytics = analytics;

            _gameOverDisplay.OnRestartButtonClicked += OnRestart;
            _character.OnDied += OnCharacterDied;
        }

        public void Dispose()
        {
            _gameOverDisplay.OnRestartButtonClicked -= OnRestart;
            _character.OnDied -= OnCharacterDied;
        }

        public void Continue()
        {
            _pauseController.Play();
            ContinueCount--;
        }

        public void GameOver()
        {
            _pauseController.Pause();
            _gameOverDisplay.Show(_score.Points);
        }

        private void OnRestart()
        {
            _sceneSwitcher.LoadGameAsync(_pauseController.Play);
            _analytics.WriteGameOver();
        }

        private void OnCharacterDied() => _pauseController.Pause();
    }
}