using UnityEngine;

namespace Assets.Source.Code_base
{
    public class EntryPoint : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private Character _character;
        [SerializeField] private ViewController _viewController;
        [SerializeField] private EnemySpawner _enemySpawner;
        [SerializeField] private PrefabsConfig _prefabsConfig;
        [SerializeField] private CharacterConfig _characterConfig;

        private IFactory _factory;
        private IInputService _input;
        private Weapon _weapon;
        private EnemyManager _enemyManager;
        private ScoreManager _scoreManager;
        private GameOverManager _gameManager;
        private PauseController _pauseController;
        private GameSceneManager _gameSceneManager;

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
            _input = new StandaloneInput();
            _scoreManager = new();
            _pauseController = new();
            _gameSceneManager = new(this);
            _factory = new Factory(_characterConfig.Weapon, _character.AttackPoint, _prefabsConfig, _character.gameObject.transform);
            _weapon = new(_input, _characterConfig.Weapon, _character, _character.AttackPoint, _character.WeaponAudioController);
            _gameManager = new(_character, _viewController, _scoreManager, _pauseController, _gameSceneManager);
        }

        private void InitEntities()
        {
            _character.Init(_input, _pauseController, _weapon);
            _viewController.Init(_character, _scoreManager);
            _enemySpawner.Init(_character.transform, _enemyManager, _pauseController, _factory);
        }
    }
}