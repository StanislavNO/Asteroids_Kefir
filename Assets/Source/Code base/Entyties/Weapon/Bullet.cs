using System;
using UnityEngine;

namespace Assets.Source.Code_base
{
    [RequireComponent(typeof(Collider))]
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _speed;

        private Transform _transform;
        private IPool _pool;

        public void Init(IPool pool)
        {
            _pool = pool;
        }

        private void Awake()
        {
            _transform = transform;
        }

        private void OnDisable()
        {
            _pool?.Put(this);
        }

        private void Update()
        {
            _transform.Translate(Vector2.up * _speed * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            gameObject.SetActive(false);
        }
    }
}