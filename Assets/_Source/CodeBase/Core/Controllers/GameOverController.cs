using Assets._Source.CodeBase.Core.Gameplay.BehaviourEffectors;
using Assets._Source.CodeBase.Core.Infrastructure.Services.SceneSwitcher;
using Assets._Source.CodeBase.Core.Infrastructure.Services.Score;
using Assets._Source.CodeBase.Core.Infrastructure.Services.TimeManager;
using Assets._Source.CodeBase.Core.View.UI;
using System;

namespace Assets._Source.CodeBase.Core.Controllers
{
    internal class GameOverController : IDisposable, IGameOverSignal, IGameOverController
    {
        public event Action GameOverring;

        private readonly IReadOnlyScore _score;
        private readonly IReadOnlyCharacter _character;
        private readonly PauseController _pauseController;
        private readonly SceneSwitcher _sceneSwitcher;
        private readonly GameOverDisplay _gameOverDisplay;

        public int ContinueCount { get; private set; } = 1;

        public GameOverController(
            IReadOnlyCharacter character,
            IReadOnlyScore score,
            PauseController pauseController,
            SceneSwitcher sceneSwitcher,
            GameOverDisplay gameOverDisplay)
        {
            _character = character;
            _score = score;
            _pauseController = pauseController;
            _sceneSwitcher = sceneSwitcher;
            _gameOverDisplay = gameOverDisplay;

            _gameOverDisplay.RestartButtonClicked += Restart;
            _character.Die += OnCharacterDie;
        }

        public void Dispose()
        {
            _gameOverDisplay.RestartButtonClicked -= Restart;
            _character.Die -= OnCharacterDie;
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

        private void Restart()
        {
            _sceneSwitcher.LoadGameAsync(_pauseController.Play);
            GameOverring?.Invoke();
        }

        private void OnCharacterDie()
        {
            _pauseController.Pause();
        }
    }
}