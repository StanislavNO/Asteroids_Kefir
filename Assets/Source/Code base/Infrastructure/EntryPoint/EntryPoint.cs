using UnityEngine;
using Zenject;

namespace Assets.Source.Code_base
{
    public sealed class EntryPoint : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private HeadsUpDisplay _hud;
        [SerializeField] private GameOverDisplay _gameOverDisplay;
        [SerializeField] private EnemySpawner _enemySpawner;
        [SerializeField] private PrefabsConfig _prefabsConfig;
        [SerializeField] private CharacterConfig _characterConfig;
        [SerializeField] private WeaponAudioController _weaponAudioController;

        [Inject] private Character _character;
        [Inject] private IInputService _input;

        private BulletFactory _bulletFactory;
        private EnemyFactory _enemyFactory;
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
            _enemyFactory.Destroy();
            _bulletFactory.Destroy();
            _enemyManager.Destroy();
            _gameManager.Destroy();
        }

        private void CreateEntities()
        {
            //_input = new StandaloneInput();
            _scoreManager = new();
            _pauseController = new();
            _gameSceneManager = new(this);
            _enemyManager = new(_scoreManager, _enemySpawner);
            _bulletFactory = new(_character.AttackPoint, _prefabsConfig);
            _enemyFactory = new(_prefabsConfig, _pauseController, _character.transform);
            _weapon = new(_input, _characterConfig.Weapon, _character, _character.AttackPoint, _bulletFactory);
            _gameManager = new(_character, _scoreManager, _pauseController, _gameSceneManager, _gameOverDisplay);
        }

        private void InitEntities()
        {
            _weaponAudioController.Init(_weapon);
            _gameOverDisplay.Init(_scoreManager);
            _character.Init(_pauseController, _weapon);
            _hud.Init(_character);
            _enemySpawner.Init(_character.transform, _enemyManager, _pauseController, _enemyFactory);
        }
    }
}