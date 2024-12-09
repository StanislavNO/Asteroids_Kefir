using System;

namespace Assets._source._code_base.Core.Controllers
{
    public interface IGameOverSignal
    {
        event Action GameOverring;
    }
}