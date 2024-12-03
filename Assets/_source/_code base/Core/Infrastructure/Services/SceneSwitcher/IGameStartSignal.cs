using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets._source._code_base.Core.Infrastructure.Services.SceneSwitcher
{
    public interface IGameStartSignal
    {
        event Action Starting;
    }
}
