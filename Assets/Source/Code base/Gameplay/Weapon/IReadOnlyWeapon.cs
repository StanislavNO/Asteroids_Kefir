using System;

namespace Assets.Source.Code_base
{
    public interface IReadOnlyWeapon
    {
        event Action LaserRecharging;
        event Action<int> LaserBulletChanged;

        int LaserBulletCount { get; }
        float LaserCooldown { get; }
    }
}
