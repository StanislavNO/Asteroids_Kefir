using System.Collections.Generic;

namespace Assets._Source.CodeBase.Meta.Services.Analytics
{
    internal interface IAnalyticProvider
    {
        void LogLaserAttack();
        void LogStartGame();
        void LogGameOver(Dictionary<CustomEventNames, int> events);
    }
}