using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Source.Code_base
{
    public interface IReadOnlyWeapon
    {
        int LaserBulletCount { get; }
        float LaserCooldown { get; }

        event Action LaserRecharging;
        event Action<int> LaserBulletChanged;
    }
}
