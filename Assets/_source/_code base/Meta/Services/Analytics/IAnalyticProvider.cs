using System.Collections.Generic;

namespace Assets._source._code_base.Meta.Services.Analytics
{
    internal interface IAnalyticProvider
    {
        void LogLaserAttack();
        void LogStartGame();
        void LogGameOver(Dictionary<CustomEventNames, int> events);
    }
}