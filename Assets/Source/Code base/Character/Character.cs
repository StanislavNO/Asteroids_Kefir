using System;
using UnityEngine;
using Zenject;

namespace Assets.Source.Code_base
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Character : MonoBehaviour, IReadOnlyCharacter, ICoroutineRunner
    {
        public event Action Die;

        private CharacterConfig _characterConfig;
        private Rotator _rotator;
        private Mover _mover;
        private Weapon _weapon;
        private IInputService _input;
        private PauseController _pauseController;

        [field: SerializeField] public WeaponAudioController WeaponAudioController { get; private set; }
        [field:SerializeField] public AttackPoint AttackPoint {get; private set;}
        public CharacterStats Stat { get; private set; }

        [Inject]
        private void Construct(IInputService input, CharacterConfig config, PauseController pauseController, Weapon weapon)
        {
            _input = input;
            _characterConfig = config;
            _weapon = weapon;
            _pauseController = pauseController;

            Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();

            _weapon.Init(AttackPoint, this);
            WeaponAudioController.Init(weapon);

            Stat = new(rigidbody, transform, _weapon);
            _rotator = new(_input, transform, _characterConfig);
            _mover = new(_input, rigidbody, transform, _characterConfig);
        }

        private void OnDestroy()
        {
            _rotator.Destroy();
            _mover.Destroy();
            _weapon.Destroy();
            StopAllCoroutines();
        }

        private void Update()
        {
            if (_pauseController.IsPause) 
                return;

            Stat.Update();
            _input.Update();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Enemy _))
                Die?.Invoke();
        }
    }
}