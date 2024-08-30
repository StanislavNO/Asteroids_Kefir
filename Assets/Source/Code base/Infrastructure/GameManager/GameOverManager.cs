namespace Assets.Source.Code_base
{
    public class GameOverManager
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

        public void Destroy()
        {
            _character.Die -= GameOver;
            _gameOverDisplay.RestartButtonClicked -= Restart;
        }

        private void Restart() =>
            _sceneManager.ReloadCurrentScene();

        private void GameOver()
        {
            _pauseController.Pause();
            _gameOverDisplay.ShowGameOverPanel();
            _gameOverDisplay.ShowScore();
        }
    }
}
