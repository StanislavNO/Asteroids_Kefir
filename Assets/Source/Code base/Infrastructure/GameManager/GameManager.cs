using UnityEngine.SceneManagement;

namespace Assets.Source.Code_base
{
    public class GameManager
    {
        private ViewController _viewController;
        private ScoreManager _scoreManager;
        private Character _character;
        private PauseController _pauseController;

        public GameManager(Character character, ViewController viewController, ScoreManager scoreManager, PauseController pauseController)
        {
            _character = character;
            _viewController = viewController;
            _scoreManager = scoreManager;
            _pauseController = pauseController;

            _character.Die += GameOver;
            _viewController.RestartButton.onClick.AddListener(Restart);
        }

        public void Destroy() =>
            _character.Die -= GameOver;

        private void GameOver()
        {
            _pauseController.Pause();
            _viewController.ShowGameOverPanel();
            _viewController.ShowScore(_scoreManager.Score);
        }

        private void Restart() =>
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
