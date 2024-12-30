using Assets._source._code_base.Meta.Services.RemoteConfig;
using Assets.Source.Code_base;
using System.Collections.Generic;
using Zenject;

namespace Assets._source._code_base.Meta
{
    internal class Bootstrap : IInitializable
    {
        private readonly List<ISDKInitializer> _initializers;
        private readonly SceneSwitcher _sceneSwitcher;
        private readonly ConfigsController _configsController;

        public Bootstrap(
            List<ISDKInitializer> initializers,
            SceneSwitcher sceneSwitcher,
            ConfigsController configsController)
        {
            _initializers = initializers;
            _sceneSwitcher = sceneSwitcher;
            _configsController = configsController;
        }

        public void Initialize() =>
            Start();

        private async void Start()
        {
            foreach (var initializer in _initializers)
                await initializer.Init();

            _configsController.InitConfigs();
            _sceneSwitcher.LoadGameAsync();
        }
    }
}