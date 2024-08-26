using UnityEngine;

namespace Assets.Source.Code_base
{
    public class EntryPoint : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private Character _character;
        [SerializeField] private PlayerAudioController _playerAudioController;
        [SerializeField] private ViewController _viewController;
        [SerializeField] private EnemySpawner _enemySpawner;

        private EnemyManager _enemyManager;
        private ScoreManager _scoreManager;
        private IInputService _input;
        private GameManager _gameManager;
        private PauseController _pauseController;

        private void Awake()
        {
            CreateEntities();
            InitEntities();
        }

        private void OnDestroy()
        {
            _gameManager.Destroy();
            _enemyManager.Destroy();
            StopAllCoroutines();
        }

        private void CreateEntities()
        {
            _pauseController = new();
            _scoreManager = new();
            _enemyManager = new(_scoreManager);
            _input = new StandaloneInput();
            _gameManager = new(_character, _viewController, _scoreManager, _pauseController);
        }

        private void InitEntities()
        {
            _character.Init(_input);
            _playerAudioController.Init(_input);
            _viewController.Init(_character);
            _enemySpawner.Init(_character.transform, _enemyManager);

            InitPauseController();
        }

        private void InitPauseController()
        {
            _pauseController.Add(_enemySpawner);
            _pauseController.Add(_character);
        }
    }
}