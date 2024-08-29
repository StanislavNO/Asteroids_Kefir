using UnityEngine;

namespace Assets.Source.Code_base
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private Character _character;
        [SerializeField] private PlayerAudioController _playerAudioController;
        [SerializeField] private ViewController _viewController;
        [SerializeField] private EnemySpawner _enemySpawner;
        [SerializeField] private PrefabsConfig _enemyConfig;

        private IInputService _input;
        private EnemyManager _enemyManager;
        private ScoreManager _scoreManager;
        private GameOverManager _gameManager;
        private PauseController _pauseController;
        private EnemyPool _enemyPool;
        private EnemyFactory _enemyFactory;

        private void Awake()
        {
            CreateEntities();
            InitEntities();
        }

        private void OnDestroy()
        {
            _gameManager.Destroy();
            _enemyManager.Destroy();
        }

        private void CreateEntities()
        {
            _scoreManager = new();
            _pauseController = new();
            _input = new StandaloneInput();
            _enemyFactory = new(_character.transform, _enemyConfig);
            _enemyPool = new(_enemyFactory);
            _enemyManager = new(_scoreManager, _enemyPool, _enemySpawner);
            _gameManager = new(_character, _viewController, _scoreManager, _pauseController);
        }

        private void InitEntities()
        {
            _character.Init(_input, _pauseController);
            _playerAudioController.Init(_input);
            _viewController.Init(_character);
            _enemySpawner.Init(_character.transform, _enemyManager, _pauseController, _enemyFactory, _enemyPool);
        }
    }
}