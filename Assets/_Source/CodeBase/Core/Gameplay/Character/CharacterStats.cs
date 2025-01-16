using UnityEngine;

namespace Assets._Source.CodeBase.Core.Gameplay.BehaviourEffectors
{
    public class CharacterStats
    {
        private readonly Rigidbody2D _rigidBody;
        private readonly Transform _transform;

        public CharacterStats(ICharacter character)
        {
            _rigidBody = character.Rigidbody;
            _transform = character.Transform;
        }

        public float Speed { get; private set; } = 0;
        public float RotationAngle { get; private set; } = 0;
        public Vector2 Position => _transform.position;

        public void Update()
        {
            ReadSpeed();
            ReadRotation();
        }

        private void ReadRotation() =>
            RotationAngle = _transform.eulerAngles.z;

        private void ReadSpeed()
        {
            float xSpeed = _rigidBody.velocity.x;
            float ySpeed = _rigidBody.velocity.y;

            Speed = Mathf.Sqrt(xSpeed * xSpeed + ySpeed * ySpeed);
        }
    }
}