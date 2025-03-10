﻿using System;
using UnityEngine;

namespace Assets._Source.CodeBase.Core.Gameplay.Enemies
{
    [RequireComponent(typeof(Collider))]
    public class Bullet : Attacker
    {
        public event Action<Bullet> OnAttackComplied;

        [SerializeField] private float _speed;

        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        private void Update()
        {
            _transform.Translate(Vector2.up * _speed * Time.deltaTime);
        }

        protected override void Attack()
        {
            base.Attack();
            OnAttackComplied?.Invoke(this);
        }
    }
}