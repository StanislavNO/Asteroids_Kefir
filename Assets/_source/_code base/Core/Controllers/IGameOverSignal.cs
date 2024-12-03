using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets._source._code_base.Core.Controllers
{
    public interface IGameOverSignal
    {
        event Action GameOverring;
    }
}
