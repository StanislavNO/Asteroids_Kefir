using Assets._Source.CodeBase.Core.Gameplay.BehaviourEffectors;
using UnityEngine;
using Zenject;

namespace Assets._Source.CodeBase.Core.Gameplay.Enemies
{
    public sealed class CharacterFollower : Enemy
    {
        private Transform _target;
        private Transform _transform;

        [Inject]
        private void Construct(ICharacterTarget character)
        {
            _target = character.Transform;
        }
        
        private void Awake() => _transform = transform;

        protected override void Move()
        {
            if (_target != null)
            {
                _transform.position = Vector3.MoveTowards(
                    _transform.position,
                    _target.position,
                    Speed * Time.fixedDeltaTime);
            }
        }
    }
}