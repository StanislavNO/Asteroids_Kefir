using System;
using UnityEngine;

namespace Assets.Source.Code_base
{
    public abstract class Enemy : MonoBehaviour, IPause
    {
        public bool _isPause = false;

        public event Action<Enemy> Died;

        [field: SerializeField] public EnemyNames Name { get; private set; }
        [field: SerializeField] public int Reward { get; private set; }
        [field: SerializeField] protected float Speed { get; private set; } = 2.5f;
        [field: SerializeField] protected Transform Transform { get; private set; }

        private void OnValidate()
        {
            if (Reward < 0)
                Reward = 0;

            if (Speed < 0)
                Speed = 0.1f;
        }

        private void FixedUpdate()
        {
            if (_isPause == false)
                Move();
        }

        public void TakeDamage() => Died?.Invoke(this);

        public void Pause(bool isPause) => _isPause = isPause;

        protected abstract void Move();
    }
}