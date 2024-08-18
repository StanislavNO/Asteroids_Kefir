using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Source.Code_base
{
    public class Rotator
    {
        private readonly IInputMover _input;
        private readonly Transform _transform;
        private readonly float _rotationSpeed = 160;

        public Rotator(IInputMover input, Transform transform)
        {
            _input = input;
            _transform = transform;

            _input.Rotating += OnMove;
        }

        public void Destroy() => _input.Rotating -= OnMove;

        public void OnMove(float horizontalInput)
        {
            float rotation = horizontalInput * _rotationSpeed * Time.deltaTime;

            _transform.Rotate(0, 0, -rotation);
        }
    }
}