using System;

namespace Assets.Source.Code_base
{
    public class GameOverHandler : IDisposable
    {
        private readonly IReadOnlyCharacter _character;
        private readonly IReadOnlyScore _score;
        private readonly PauseController _pauseController;
        private readonly SceneSwitcher _sceneSwitcher;
        private readonly GameOverDisplay _gameOverDisplay;

        public GameOverHandler(IReadOnlyCharacter character, IReadOnlyScore score, PauseController pauseController, SceneSwitcher sceneSwitcher, GameOverDisplay gameOverDisplay)
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

        private void Restart() =>
            _sceneSwitcher.LoadGameAsync(_pauseController.Play);

        private void GameOver()
        {
            _pauseController.Pause();
            _gameOverDisplay.ShowGameOverPanel();
            _gameOverDisplay.ShowScore(_score.Points);
        }
    }
}