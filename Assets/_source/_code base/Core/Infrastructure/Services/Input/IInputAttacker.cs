using System;

namespace Assets.Source.Code_base
{
    public interface IInputAttacker
    {
        event Action DefaultAttacking;
        event Action HardAttacking;
    }
}