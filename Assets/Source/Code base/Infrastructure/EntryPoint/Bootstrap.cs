using UnityEngine;

namespace Assets.Source.Code_base
{
    public class Bootstrap : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private Character _character;
        [SerializeField] private ViewStatsPanelController _statsPanel;

        private void Awake()
        {
            _character.Init(new StandaloneInput());
            _statsPanel.Init(_character);
        }
    }
}