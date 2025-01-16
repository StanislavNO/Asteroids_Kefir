using Assets._Source.CodeBase.Core.Gameplay.BehaviourEffectors;
using Assets._Source.CodeBase.Core.Gameplay.Enemies;
using Assets._Source.CodeBase.Core.Infrastructure.Services.Input;
using Assets._Source.CodeBase.Core.Infrastructure.Services.TimeManager;
using Assets._Source.CodeBase.Core.View.UI;
using System;
using Zenject;

namespace Assets._Source.CodeBase.Core.Controllers
{
    public class PlayerInputController : IInitializable, IDisposable
    {
        private readonly IInputService _input;
        private readonly IReadOnlyPause _time;
        private readonly IGameDisplay _display;
        private readonly IReadOnlyCharacter _character;
        private readonly IWeapon _weapon;
        private readonly Mover _mover;
        private readonly Rotator _rotator;

        public PlayerInputController(
            IInputService inputService,
            IReadOnlyCharacter character,
            IWeapon weapon,
            IReadOnlyPause pause,
            Mover mover,
            Rotator rotator,
            IGameDisplay display)
        {
            _input = inputService;
            _character = character;
            _weapon = weapon;
            _time = pause;
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
                _display.ShowLaserBullet(_weapon.LaserBulletCount);
        }

        private void OnDefoldAttackClicked()
        {
            if (_time.IsPause)
                return;

            _weapon.AttackDefold();
        }
    }
}