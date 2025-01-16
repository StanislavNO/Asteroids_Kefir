using Assets._Source.CodeBase.Core.Infrastructure.Services.TimeManager;
using System;
using UnityEngine;

namespace Assets._Source.CodeBase.Core.Gameplay.Enemies
{
    public abstract class Enemy : MonoBehaviour
    {
        public event Action<Enemy> Died;

        private IReadOnlyPause _time;

        [field: SerializeField] public EnemyNames Name { get; private set; }
        [field: SerializeField] public int Reward { get; private set; }
        [field: SerializeField] protected float Speed { get; private set; } = 2.5f;
        [field: SerializeField] protected Transform Transform { get; private set; }

        public void Init(IReadOnlyPause pauseController)
        {
            _time = pauseController;
        }

        private void OnValidate()
        {
            if (Reward < 0)
                Reward = 0;

            if (Speed < 0)
                Speed = 0.1f;
        }

        private void FixedUpdate()
        {
            if (_time.IsPause == false)
                Move();
        }

        public void TakeDamage() => Died?.Invoke(this);

        protected abstract void Move();
    }
}