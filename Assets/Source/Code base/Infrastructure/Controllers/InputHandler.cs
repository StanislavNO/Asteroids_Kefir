using System;
using Zenject;

namespace Assets.Source.Code_base.Infrastructure.Controllers
{
    public class InputHandler : IInitializable, IDisposable
    {
        private readonly IInputService _input;
        private readonly IReadOnlyPause _pause;
        private readonly AudioController _audioController;
        private readonly Character _character;
        private readonly Weapon _weapon;
        private readonly Mover _mover;
        private readonly Rotator _rotator;

        public InputHandler(IInputService inputService, Character character, Weapon weapon, IReadOnlyPause pause, AudioController audioController, Mover mover, Rotator rotator)
        {
            _input = inputService;
            _character = character;
            _weapon = weapon;
            _pause = pause;
            _audioController = audioController;
            _mover = mover;
            _rotator = rotator;
        }

        public void Initialize()
        {
            _input.DefaultAttacking += OnDefoldAttackClicked;
            _input.HardAttacking += OnHardAttackClicked;
            _input.Moving += OnMoveClicking;
            _input.Rotating += OnRotateClicking;
        }

        public void Dispose()
        {
            _input.DefaultAttacking -= OnDefoldAttackClicked;
            _input.HardAttacking -= OnHardAttackClicked;
            _input.Moving -= OnMoveClicking;
            _input.Rotating -= OnRotateClicking;
        }

        private void OnRotateClicking(float rotateAxis) =>
            _rotator.Rotate(rotateAxis);

        private void OnMoveClicking(float moveAxis) =>
            _mover.Move(moveAxis);

        private void OnHardAttackClicked()
        {
            if (_pause.IsPause)
                return;

            _weapon.AttackLaser();
            _audioController.PlayLaserAttack(_weapon.TimeWorkLaser);
        }

        private void OnDefoldAttackClicked()
        {
            if (_pause.IsPause)
                return;

            _weapon.AttackDefold();
            _audioController.PlayBulletAttack();
        }
    }
}