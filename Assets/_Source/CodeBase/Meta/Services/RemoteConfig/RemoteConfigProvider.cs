using Assets._Source.CodeBase.Core.Common.Configs;
using Assets._Source.CodeBase.Meta.Services.JsonManager;
using Firebase.RemoteConfig;

namespace Assets._Source.CodeBase.Meta.Services.RemoteConfig
{
    internal class RemoteConfigProvider
    {
        private readonly IJsonConvector _convector;

        private GameConfigData _gameData;

        public MovementConfig MovementConfig { get; private set; }
        public WeaponConfig WeaponConfig { get; private set; }

        public RemoteConfigProvider(IJsonConvector jsonConvector)
        {
            _convector = jsonConvector;

            _gameData = new GameConfigData();
            MovementConfig = new MovementConfig();
            WeaponConfig = new WeaponConfig();
        }

        public void Load()
        {
            string json = LoadJsonConfig(RemoteValues.game_configs);
            _gameData = ReadConfig(json);

            SetValue();
        }

        private void SetValue()
        {
            MovementConfig.SetValues(
                _gameData.Drag,
                _gameData.MaxSpeed,
                _gameData.Acceleration,
                _gameData.RotationSpeed);

            WeaponConfig.SetValues(
                _gameData.LaserCooldown,
                _gameData.LaserBulletCount,
                _gameData.TimeWorkLaser);
        }

        private string LoadJsonConfig(RemoteValues name) =>
            FirebaseRemoteConfig.DefaultInstance.GetValue(name.ToString()).StringValue;

        private GameConfigData ReadConfig(string json) =>
            _convector.Get(json);
    }
}