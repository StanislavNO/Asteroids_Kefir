using Assets.Source.Code_base;
using Firebase.RemoteConfig;
using UnityEngine;

namespace Assets._source._code_base.Meta.Services.RemoteConfig
{
    internal class RemoteConfigProvider
    {
        private float _drag;
        private float _maxSpeed;
        private float _acceleration;
        private float _rotationSpeed;
        private float _laserCooldown;
        private int _laserBulletCount;
        private int _timeWorkLaser;
        private int _continueCount;

        public MovementConfig MovementConfig { get; private set; }
        public WeaponConfig WeaponConfig { get; private set; }

        public RemoteConfigProvider()
        {
            MovementConfig = new MovementConfig();
            WeaponConfig = new WeaponConfig();
        }

        public void Load()
        {
            _drag = Load(RemoteValues.Drag);
            _maxSpeed = Load(RemoteValues.Max_Speed);
            _acceleration = Load(RemoteValues.Acceleration);
            _rotationSpeed = Load(RemoteValues.Rotation_Speed);
            _laserCooldown = Load(RemoteValues.Laser_Cooldown);
            _laserBulletCount = (int)Load(RemoteValues.Laser_Bullet_Count);
            _timeWorkLaser = (int)Load(RemoteValues.Time_Work_Laser);
            _continueCount = (int)Load(RemoteValues.Continue_Count);
            Debug.Log(_laserBulletCount);
            SetValue();
        }

        private void SetValue()
        {
            MovementConfig.SetValues(_drag, _maxSpeed, _acceleration, _rotationSpeed);
            WeaponConfig.SetValues(_laserCooldown, _laserBulletCount, _timeWorkLaser);
        }

        private float Load(RemoteValues name) =>
            (float)FirebaseRemoteConfig.DefaultInstance.GetValue(name.ToString()).DoubleValue;
    }
}