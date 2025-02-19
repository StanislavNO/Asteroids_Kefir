using System;

namespace Assets._Source.CodeBase.Core.Controllers
{
    public interface IGameOverSignal
    {
        event Action OnGameOver;
    }
}