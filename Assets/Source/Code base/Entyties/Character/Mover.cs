using System;
using UnityEngine;

namespace Assets.Source.Code_base
{
    public class Mover
    {
        private const float DRAG = 1f;
        private const float MAX_SPEED = 4.5f;
        private const float ACCELERATION = 1f;

        private readonly Rigidbody2D _rigidbody;
        private readonly Transform _transform;
        private readonly IInputMover _input;

        public Mover(IInputMover input, Rigidbody2D rigidbody, Transform transform)
        {
            _rigidbody = rigidbody;
            _transform = transform;
            _rigidbody.drag = DRAG;
            _input = input;

            _input.Moving += OnMove;
        }

        public void Destroy() => _input.Moving += OnMove;

        private void OnMove(float verticalInput)
        {
            if (verticalInput > 0)
            {
                Vector2 force = _transform.up * Math.Abs(verticalInput) * ACCELERATION;
                _rigidbody.AddForce(force, ForceMode2D.Impulse);
            }

            if (_rigidbody.velocity.magnitude > MAX_SPEED)
                _rigidbody.velocity = _rigidbody.velocity.normalized * MAX_SPEED;
        }
    }
}