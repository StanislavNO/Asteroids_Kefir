using System;

namespace Assets._Source.CodeBase.Core.Gameplay.Enemies
{
    public interface IAttackObserver
    {
        event Action<float> LaserAttacking;
        event Action DefaultAttacking;
    }
}