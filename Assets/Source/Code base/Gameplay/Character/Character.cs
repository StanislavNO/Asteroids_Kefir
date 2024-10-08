using Assets.Source.Code_base.Gameplay.Character;
using System;
using UnityEngine;

namespace Assets.Source.Code_base
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Character : MonoBehaviour, IReadOnlyCharacter, IMovement
    {
        public event Action Die;

        [SerializeField] private WeaponAudioController _weaponAudioController;
        [SerializeField] private AttackPoint _attackPoint;

        private CharacterConfig _characterConfig;
        private Rotator _rotator;
        private Mover _mover;
        private Weapon _weapon;

        public CharacterStats Stat { get; private set; }

        public void Initialize(CharacterConfig config, Weapon weapon)
        {
            _characterConfig = config;
            _weapon = weapon;

            Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();

            _weapon.Init(_attackPoint);
            _weaponAudioController.Init(weapon);

            Stat = new(rigidbody, transform, _weapon);
            _rotator = new(transform, _characterConfig);
            _mover = new(rigidbody, transform, _characterConfig);
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