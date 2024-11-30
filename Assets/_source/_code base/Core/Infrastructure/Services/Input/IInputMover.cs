using System;

namespace Assets.Source.Code_base
{
    public interface IInputMover
    {
        event Action<float> Rotating;
        event Action<float> Moving;
    }
}