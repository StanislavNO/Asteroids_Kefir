using UnityEngine;

namespace Assets.Source.Code_base
{
    public class EntryPoint : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private Character _character;
        [SerializeField] private PlayerAudioController _playerAudioController;
        [SerializeField] private ViewStatsPanelController _statsPanel;
        [SerializeField] private EnemySpawner _enemySpawner;

        private EnemyManager _enemyManager;
        private ScoreManager _scoreManager;
        private IInputService _input;

        private void Awake()
        {
            _scoreManager = new();
            _enemyManager = new(_scoreManager);
            _input = new StandaloneInput();

            _character.Init(_input);
            _playerAudioController.Init(_input);
            _statsPanel.Init(_character);
            _enemySpawner.Init(_character.transform, _enemyManager);
        }

        private void OnDestroy()
        {
            _enemyManager.Destroy();
            StopAllCoroutines();
        }
    }
}