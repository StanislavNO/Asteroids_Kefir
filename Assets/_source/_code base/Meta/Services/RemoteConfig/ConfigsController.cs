using Assets.Source.Code_base;
using UnityEngine;

namespace Assets._source._code_base.Meta.Services.RemoteConfig
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
            Debug.Log(_configProvider.WeaponConfig.LaserBulletCount);
            _characterConfig.SetConfigs(
                _configProvider.MovementConfig,
                _configProvider.WeaponConfig);
        }
    }
}