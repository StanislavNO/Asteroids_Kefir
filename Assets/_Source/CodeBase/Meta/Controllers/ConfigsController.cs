using Assets._Source.CodeBase.Core.Common.Configs;

namespace Assets._Source.CodeBase.Meta.Services.RemoteConfig
{
    internal class ConfigsController
    {
        private readonly CharacterConfig _characterConfig;
        private readonly RemoteConfigProvider _configProvider;

        public ConfigsController(CharacterConfig characterConfig, RemoteConfigProvider remoteConfigProvider)
        {
            _characterConfig = characterConfig;
            _configProvider = remoteConfigProvider;
        }

        public void InitConfigs()
        {
            _configProvider.Load();

            _characterConfig.SetConfigs(
                _configProvider.MovementConfig,
                _configProvider.WeaponConfig);
        }
    }
}