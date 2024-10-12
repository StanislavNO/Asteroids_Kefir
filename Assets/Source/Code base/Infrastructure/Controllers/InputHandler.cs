using System;
using Zenject;

namespace Assets.Source.Code_base.Infrastructure.Controllers
{
    public class InputHandler : IInitializable, IDisposable
    {
        private readonly IInputService _input;
        private readonly IReadOnlyPause _time;
        private readonly IDisplay _display;
        private readonly AudioController _audioController;
        private readonly Character _character;
        private readonly Weapon _weapon;
        private readonly Mover _mover;
        private readonly Rotator _rotator;

        public InputHandler(IInputService inputService, Character character, Weapon weapon, IReadOnlyPause pause, AudioController audioController, Mover mover, Rotator rotator, IDisplay display)
        {
            _input = inputService;
            _character = character;
            _weapon = weapon;
            _time = pause;
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

        private void OnRotateClicking(float rotateAxis)
        {
            _rotator.Rotate(rotateAxis);
            _display.ShowRotation(_character.Stat.RotationAngle);
        }

        private void OnMoveClicking(float moveAxis) =>
            _mover.Move(moveAxis);

        private void OnHardAttackClicked()
        {
            if (_time.IsPause)
                return;

            _weapon.AttackLaser();
            _audioController.PlayLaserAttack(_weapon.TimeWorkLaser);
            _display.ShowLaserCooldown(_weapon.LaserCooldown);
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