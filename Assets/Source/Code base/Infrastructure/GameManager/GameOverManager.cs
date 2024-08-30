namespace Assets.Source.Code_base
{
    public class GameOverManager
    {
        private readonly ViewController _viewController;
        private readonly ScoreManager _scoreManager;
        private readonly Character _character;
        private readonly PauseController _pauseController;
        private readonly GameSceneManager _sceneManager;

        public GameOverManager(Character character, ViewController viewController, ScoreManager scoreManager, PauseController pauseController, GameSceneManager sceneManager)
        {
            _character = character;
            _viewController = viewController;
            _scoreManager = scoreManager;
            _pauseController = pauseController;
            _sceneManager = sceneManager;

            _viewController.RestartButton.onClick.AddListener(Restart);
            _character.Die += GameOver;
        }

        public void Destroy() =>
            _character.Die -= GameOver;

        private void GameOver()
        {
            _pauseController.Pause();
            _viewController.ShowGameOverPanel();
            _viewController.ShowScore();
        }

        private void Restart() =>
            _sceneManager.ReloadCurrentScene();
    }
}
