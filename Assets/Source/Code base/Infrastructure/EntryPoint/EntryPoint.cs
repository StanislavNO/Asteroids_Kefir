using UnityEngine;

namespace Assets.Source.Code_base
{
    public class EntryPoint : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private Character _character;
        [SerializeField] private HeadsUpDisplay _hud;
        [SerializeField] private GameOverDisplay _gameOverDisplay;
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
            _enemyManager = new(_scoreManager, _enemySpawner);
            _factory = new Factory(_characterConfig.Weapon, _character.AttackPoint, _prefabsConfig, _character.gameObject.transform, _pauseController);
            _weapon = new(_input, _characterConfig.Weapon, _character, _character.AttackPoint, _character.WeaponAudioController);
            _gameManager = new(_character, _scoreManager, _pauseController, _gameSceneManager, _gameOverDisplay);
        }

        private void InitEntities()
        {
            _gameOverDisplay.Init(_scoreManager);
            _character.Init(_input, _pauseController, _weapon);
            _hud.Init(_character);
            _enemySpawner.Init(_character.transform, _enemyManager, _pauseController, _factory);
        }
    }
}