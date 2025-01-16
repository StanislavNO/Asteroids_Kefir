using System;

namespace Assets._Source.CodeBase.Core.Infrastructure.Services.SceneSwitcher
{
    public interface IGameStartSignal
    {
        event Action Starting;
    }
}