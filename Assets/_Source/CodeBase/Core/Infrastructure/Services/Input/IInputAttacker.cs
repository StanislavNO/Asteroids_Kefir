using System;

namespace Assets._Source.CodeBase.Core.Infrastructure.Services.Input
{
    public interface IInputAttacker
    {
        event Action DefaultAttacking;
        event Action HardAttacking;
    }
}