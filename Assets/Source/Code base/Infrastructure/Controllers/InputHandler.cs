using System;
using Zenject;

namespace Assets.Source.Code_base.Infrastructure.Controllers
{
    public class InputHandler : IInitializable, IDisposable
    {
        private readonly IInputService _input;
        private readonly AudioController _audioController;
        private readonly Character _character;
        private readonly Weapon _weapon;
        private readonly PauseController _pause;

        public InputHandler(IInputService inputService, Character character, Weapon weapon, PauseController pause, AudioController audioController)
        {
            _input = inputService;
            _character = character;
            _weapon = weapon;
            _pause = pause;
            _audioController = audioController;
        }

        public void Initialize()
        {
            _input.DefaultAttacking += OnDefoldAttackClicking;
        }

        public void Dispose()
        {
            _input.DefaultAttacking -= OnDefoldAttackClicking;
        }

        private void OnDefoldAttackClicking()
        {
            if (_pause.IsPause)
                return;

            _weapon.AttackDefold();
            _audioController.PlayBulletAttack();
        }


    }
}