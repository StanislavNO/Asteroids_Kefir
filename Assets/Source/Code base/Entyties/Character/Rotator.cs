using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Source.Code_base
{
    public class Rotator
    {
        private readonly IInputMover _input;
        private readonly Transform _transform;
        private readonly float _rotationSpeed;

        public Rotator(IInputMover input, Transform transform, CharacterConfig data)
        {
            _input = input;
            _transform = transform;
            _rotationSpeed = data.Movement.RotationSpeed;

            _input.Rotating += OnRotate;
        }

        public void Destroy() => _input.Rotating -= OnRotate;

        public void OnRotate(float horizontalInput)
        {
            float rotation = horizontalInput * _rotationSpeed * Time.deltaTime;

            _transform.Rotate(0, 0, -rotation);
        }
    }
}