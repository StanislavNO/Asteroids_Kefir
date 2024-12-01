using System;

namespace Assets.Source.Code_base
{
    public interface IAttackObserver
    {
        event Action<float> LaserAttacking;
        event Action DefaultAttacking;
    }
}