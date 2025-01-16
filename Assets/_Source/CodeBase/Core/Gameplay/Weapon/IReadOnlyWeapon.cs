using System;

namespace Assets._Source.CodeBase.Core.Gameplay.Enemies
{
    public interface IReadOnlyWeapon
    {
        event Action<float> LaserRecharging;
        event Action<int> LaserBulletChanged;

        int LaserBulletCount { get; }
        float LaserCooldown { get; }
        float TimeWorkLaser { get; }
    }
}