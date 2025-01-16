using System;
using UnityEngine;
using Zenject;

namespace Assets._Source.CodeBase.Core.Infrastructure.Services.Input
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
            if (UnityEngine.Input.GetKeyDown(KeyCode.Mouse0))
                DefaultAttacking?.Invoke();

            if (UnityEngine.Input.GetKeyDown(KeyCode.Mouse1))
                HardAttacking?.Invoke();
        }

        private void HandleMoveInput()
        {
            _moveAxis = UnityEngine.Input.GetAxis(VERTICAL);
            _rotationAxis = UnityEngine.Input.GetAxis(HORIZONTAL);

            Moving?.Invoke(_moveAxis);
            Rotating?.Invoke(_rotationAxis);
        }
    }
}