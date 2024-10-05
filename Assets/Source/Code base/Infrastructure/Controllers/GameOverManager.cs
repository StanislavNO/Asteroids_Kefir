using System;

namespace Assets.Source.Code_base
{
    public class GameOverManager : IDisposable
    {
        private readonly Character _character;
        private readonly PauseController _pauseController;
        private readonly GameSceneManager _sceneManager;
        private readonly GameOverDisplay _gameOverDisplay;

        public GameOverManager(Character character, ScoreManager scoreManager, PauseController pauseController, GameSceneManager sceneManager, GameOverDisplay gameOverDisplay)
        {
            _character = character;
            _pauseController = pauseController;
            _sceneManager = sceneManager;
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
            _sceneManager.ReloadCurrentScene();
            _pauseController.Play();
        }

        private void GameOver()
        {
            _pauseController.Pause();
            _gameOverDisplay.ShowGameOverPanel();
            _gameOverDisplay.ShowScore();
        }
    }
}
