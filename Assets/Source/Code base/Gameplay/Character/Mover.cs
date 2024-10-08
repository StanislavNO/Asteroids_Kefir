using System;
using UnityEngine;

namespace Assets.Source.Code_base
{
    public class Mover
    {
        private readonly float _maxSpeed;
        private readonly float _acceleration;
        private readonly Rigidbody2D _rigidbody;
        private readonly Transform _transform;

        public Mover( Rigidbody2D rigidbody, Transform transform, CharacterConfig data)
        {
            _rigidbody = rigidbody;
            _transform = transform;
            _rigidbody.drag = data.Movement.Drag;
            _maxSpeed = data.Movement.MaxSpeed;
            _acceleration = data.Movement.Acceleration;
        }

        public void Move(float verticalAxis)
        {
            if (verticalAxis > 0)
            {
                Vector2 force = _transform.up * Math.Abs(verticalAxis) * _acceleration;
                _rigidbody.AddForce(force, ForceMode2D.Impulse);
            }

            if (_rigidbody.velocity.magnitude > _maxSpeed)
                _rigidbody.velocity = _rigidbody.velocity.normalized * _maxSpeed;
        }
    }
}