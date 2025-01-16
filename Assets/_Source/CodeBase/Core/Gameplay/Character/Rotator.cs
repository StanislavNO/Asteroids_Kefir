using Assets._Source.CodeBase.Core.Common.Configs;
using UnityEngine;

namespace Assets._Source.CodeBase.Core.Gameplay.BehaviourEffectors
{
    public class Rotator
    {
        private readonly Transform _transform;
        private readonly float _rotationSpeed;

        public Rotator(ICharacterTarget character, CharacterConfig data)
        {
            _transform = character.Transform;
            _rotationSpeed = data.Movement.RotationSpeed;
        }

        public void Rotate(float horizontalAxis)
        {
            float rotation = horizontalAxis * _rotationSpeed * Time.deltaTime;

            _transform.Rotate(0, 0, -rotation);
        }
    }
}