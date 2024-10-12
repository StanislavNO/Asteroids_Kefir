using Assets.Source.Code_base.Gameplay.Character;
using System;
using UnityEngine;
using Zenject;

namespace Assets.Source.Code_base
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Character : MonoBehaviour, IReadOnlyCharacter, IMovement, ICharacterTarget
    {
        public event Action Die;

        [SerializeField] private AttackPoint _attackPoint;

        private CharacterConfig _characterConfig;
        private Rotator _rotator;
        private Mover _mover;
        private IWeapon _weapon;

        public CharacterStats Stat { get; private set; }
        public Transform Transform { get; private set; }
        public Rigidbody2D Rigidbody { get; private set; }

        [Inject]
        public void Construct(CharacterConfig config, IWeapon weapon)
        {
            _characterConfig = config;
            _weapon = weapon;

            Transform = transform;
            Rigidbody = GetComponent<Rigidbody2D>();

            _weapon.Init(_attackPoint);
            Stat = new(GetComponent<Rigidbody>(), transform);
            _rotator = new(transform, _characterConfig);
            _mover = new(GetComponent<Rigidbody>(), transform, _characterConfig);
        }

        private void Update()
        {
            Stat.Update();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Enemy _))
                Die?.Invoke();
        }

        public void Move(float verticalAxis) =>
            _mover.Move(verticalAxis);

        public void Rotate(float horizontalAxis) =>
            _rotator.Rotate(horizontalAxis);
    }
}