﻿using Assets._Source.CodeBase.Core.Gameplay.BehaviourEffectors;
using Assets._Source.CodeBase.Core.Gameplay.Enemies;
using Assets._Source.CodeBase.Core.View.UI;
using System;
using Zenject;

namespace Assets._Source.CodeBase.Core.Controllers
{
    public class GameViewController : IInitializable, IFixedTickable, IDisposable
    {
        private readonly IReadOnlyCharacter _character;
        private readonly IReadOnlyWeapon _weapon;
        private readonly HeadsUpDisplay _display;

        public GameViewController(HeadsUpDisplay display, IReadOnlyCharacter character, IReadOnlyWeapon weapon)
        {
            _character = character;
            _display = display;
            _weapon = weapon;
        }

        public void Initialize()
        {
            UpdateView();
            _display.ShowLaserBullet(_weapon.LaserBulletCount);

            _weapon.OnLaserRecharging += OnAttackRecharged;
            _weapon.OnLaserBulletChanged += OnBulletChanged;
        }

        public void Dispose()
        {
            _weapon.OnLaserRecharging -= OnAttackRecharged;
            _weapon.OnLaserBulletChanged -= OnBulletChanged;
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

        private void OnAttackRecharged(float duration) =>
            _display.WriteWeaponCooldown(duration);
    }
}