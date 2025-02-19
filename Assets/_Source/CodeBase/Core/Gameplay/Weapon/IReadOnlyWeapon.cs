using System;

namespace Assets._Source.CodeBase.Core.Gameplay.Enemies
{
    public interface IReadOnlyWeapon
    {
        event Action<float> OnLaserRecharging;
        event Action<int> OnLaserBulletChanged;

        int LaserBulletCount { get; }
        float LaserCooldown { get; }
        float TimeWorkLaser { get; }
    }
}