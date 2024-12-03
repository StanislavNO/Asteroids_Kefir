using Assets._source._code_base.Core.Controllers;
using System;

namespace Assets.Source.Code_base
{
    internal class GameOverController : IDisposable, IGameOverSignal
    {
        public event Action GameOverring;

        private readonly IReadOnlyCharacter _character;
        private readonly IReadOnlyScore _score;
        private readonly PauseController _pauseController;
        private readonly SceneSwitcher _sceneSwitcher;
        private readonly GameOverDisplay _gameOverDisplay;

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

            _character.Die += GameOver;
            _gameOverDisplay.RestartButtonClicked += Restart;
        }

        public void Dispose()
        {
            _character.Die -= GameOver;
            _gameOverDisplay.RestartButtonClicked -= Restart;
        }

        private void Restart()
        {
            _sceneSwitcher.LoadGameAsync(_pauseController.Play);
            GameOverring?.Invoke();
        }

        private void GameOver()
        {
            _pauseController.Pause();
            _gameOverDisplay.Show(_score.Points);
        }
    }
}