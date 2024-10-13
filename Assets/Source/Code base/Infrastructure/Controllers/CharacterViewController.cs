using System;
using Zenject;

namespace Assets.Source.Code_base
{
    public class CharacterViewController : IInitializable, IFixedTickable, IDisposable
    {
        private readonly IReadOnlyCharacter _character;
        private readonly IReadOnlyWeapon _weapon;
        private readonly IDisplay _display;

        public CharacterViewController(IDisplay display, IReadOnlyCharacter character, IReadOnlyWeapon weapon)
        {
            _character = character;
            _display = display;
            _weapon = weapon;
        }

        public void Initialize()
        {
            UpdateView();
            _weapon.LaserRecharging += OnAttackRecharging;
            _weapon.LaserBulletChanged += OnBulletChanged;
        }

        public void Dispose()
        {
            _weapon.LaserRecharging -= OnAttackRecharging;
            _weapon.LaserBulletChanged -= OnBulletChanged;
        }

        public void FixedTick()
        {
            UpdateView();
        }

        private void UpdateView()
        {
            _display.ShowCoordinate(_character.Stat.Position);
            _display.ShowSpeed(_character.Stat.Speed);
        }

        private void OnBulletChanged(int count) =>
            _display.ShowLaserBullet(count);

        private void OnAttackRecharging(float duration) =>
            _display.ReadWeaponCooldown(duration);
    }
}