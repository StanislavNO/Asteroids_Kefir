using System;
using UnityEngine;

namespace Assets._Source.CodeBase.Core.Gameplay.BehaviorEffectors
{
    public class ObjectRotator : MonoBehaviour
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private float _speed;
        [SerializeField][Range(-1, 1)] private int _reverse;

        private void Update() => Rotate();

        private void Rotate()
        {
            float rotation = _reverse * _speed * Time.deltaTime;
            _transform.Rotate(Vector3.forward, rotation);
        }
    }
}
