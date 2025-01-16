using System;

namespace Assets._Source.CodeBase.Core.Infrastructure.Services.Input
{
    public interface IInputMover
    {
        event Action<float> Rotating;
        event Action<float> Moving;
    }
}