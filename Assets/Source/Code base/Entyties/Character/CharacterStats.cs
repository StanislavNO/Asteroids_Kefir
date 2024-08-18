using System;
using UnityEngine;

namespace Assets.Source.Code_base
{
    public class CharacterStats
    {
        private readonly Rigidbody2D _rigidBody;
        private readonly Transform _transform;
        private readonly IReadOnlyWeapon _weapon;

        public CharacterStats(Rigidbody2D rigidBody, Transform transform, IReadOnlyWeapon weapon)
        {
            _rigidBody = rigidBody;
            _transform = transform;
            _weapon = weapon;
        }

        public float Speed { get; private set; } = 0;
        public float RotationAngle { get; private set; } = 0;
        public Vector2 Position => _transform.position;
        public int LaserPoint { get; private set; } = 0;
        public float LaserCooldown { get; private set; } = 0;

        public void Update()
        {
            ReadSpeed();
            ReadRotation();
        }

        public void ReadRotation()
        {
            RotationAngle = _transform.eulerAngles.z;
        }

        private void ReadSpeed()
        {
            float xSpeed = _rigidBody.velocity.x;
            float ySpeed = _rigidBody.velocity.y;

            Speed = Mathf.Sqrt(xSpeed * xSpeed + ySpeed * ySpeed);
        }
    }
}
