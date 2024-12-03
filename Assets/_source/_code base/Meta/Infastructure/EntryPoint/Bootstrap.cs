using Assets.Source.Code_base;
using UnityEngine;
using Zenject;

namespace Assets._source._code_base.Meta
{
    internal class Bootstrap : MonoBehaviour
    {
        private FirebaseInitializer _initializer;
        private SceneSwitcher _sceneSwitcher;

        [Inject]
        private void Construct(FirebaseInitializer initializer, SceneSwitcher sceneSwitcher)
        {
            _initializer = initializer;
            _sceneSwitcher = sceneSwitcher;
        }

        private async void Start()
        {
            await _initializer.Init();
            _sceneSwitcher.LoadGameAsync();
        }
    }
}