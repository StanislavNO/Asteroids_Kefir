using Assets._Source.CodeBase.Core.Infrastructure.Services.SceneSwitcher;
using Assets._Source.CodeBase.Meta.Services.RemoteConfig;
using System.Collections.Generic;
using Zenject;

namespace Assets._Source.CodeBase.Meta.Infrastructure.EntryPoint
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

        public void Initialize() => Start();

        private async void Start()
        {
            foreach (ISDKInitializer initializer in _initializers)
                await initializer.Init();

            _configsController.InitConfigs();
            _sceneSwitcher.LoadGameAsync();
        }
    }
}