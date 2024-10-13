using System;
using Zenject;

namespace Assets.Source.Code_base.Infrastructure.Controllers
{
    public class InputHandler : IInitializable, IDisposable
    {
        private readonly IInputService _input;
        private readonly IReadOnlyPause _time;
        private readonly IDisplay _display;
        private readonly IReadOnlyCharacter _character;
        private readonly IWeapon _weapon;
        private readonly AudioController _audioController;
        private readonly Mover _mover;
        private readonly Rotator _rotator;

        public InputHandler(IInputService inputService, IReadOnlyCharacter character, IWeapon weapon, IReadOnlyPause pause, AudioController audioController, Mover mover, Rotator rotator, IDisplay display)
        {
            _input = inputService;
            _character = character;
            _weapon = weapon;
            _time = pause;
            _audioController = audioController;
            _mover = mover;
            _rotator = rotator;
            _display = display;
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

        private void OnRotateClicking(float rotateAxis)
        {
            if (_time.IsPause)
                return;

            _rotator.Rotate(rotateAxis);
            _display.ShowRotation(_character.Stat.RotationAngle);
        }

        private void OnMoveClicking(float moveAxis)
        {
            if (_time.IsPause == false)
                _mover.Move(moveAxis);
        }

        private void OnHardAttackClicked()
        {
            if (_time.IsPause)
                return;

            if (_weapon.TryAttackLaser())
                _audioController.PlayLaserAttack(_weapon.TimeWorkLaser);

            _display.ShowLaserBullet(_weapon.LaserBulletCount);
        }

        private void OnDefoldAttackClicked()
        {
            if (_time.IsPause)
                return;

            _weapon.AttackDefold();
            _audioController.PlayBulletAttack();
        }
    }
}