﻿using System;
using UnityEngine;
using Zenject;

namespace Assets.Source.Code_base
{
    public class StandaloneInput : IInputService, ITickable
    {
        private const string VERTICAL = "Vertical";
        private const string HORIZONTAL = "Horizontal";

        public event Action DefaultAttacking;
        public event Action HardAttacking;
        public event Action<float> Moving;
        public event Action<float> Rotating;

        private float _moveAxis = 0f;
        private float _rotationAxis = 0f;

        public void Tick()
        {
            HandleAttackInput();
            HandleMoveInput();
        }

        private void HandleAttackInput()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
                DefaultAttacking?.Invoke();

            if (Input.GetKeyDown(KeyCode.Mouse1))
                HardAttacking?.Invoke();
        }

        private void HandleMoveInput()
        {
            _moveAxis = Input.GetAxis(VERTICAL);
            _rotationAxis = Input.GetAxis(HORIZONTAL);

            Moving?.Invoke(_moveAxis);
            Rotating?.Invoke(_rotationAxis);
        }
    }
}