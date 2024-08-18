using System;

namespace Assets.Source.Code_base
{
    public interface IInputAttacker
    {
        event Action DefoldAttacking;
        event Action HardAttacking;
    }
}