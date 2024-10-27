using System;
using UnityEngine;

namespace Assets.Source.Code_base
{
    [Serializable]
    public class MovementConfig
    {
        [SerializeField][Range(0, 10)] private float _drag = 1f;
        [SerializeField][Range(1, 10)] private float _maxSpeed = 4.5f;
        [SerializeField][Range(1, 10)] private float _acceleration = 1f;
        [SerializeField][Range(1, 360)] private float _rotationSpeed = 160;

        public float Drag => _drag;
        public float MaxSpeed => _maxSpeed;
        public float Acceleration => _acceleration;
        public float RotationSpeed => _rotationSpeed;
    }
}