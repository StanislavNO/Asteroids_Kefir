using UnityEngine;

namespace Assets._Source.CodeBase.Core.Gameplay.Enemies
{
    public sealed class CharacterFollower : Enemy
    {
        private Transform _character;
        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        public void SetTarget(Transform target) => _character = target;

        protected override void Move()
        {
            if (_character != null)
            {
                _transform.position = Vector3.MoveTowards(
                    _transform.position,
                    _character.position,
                    Speed * Time.fixedDeltaTime);
            }
        }
    }
}