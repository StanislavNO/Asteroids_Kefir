using Firebase.Analytics;
using System.Collections.Generic;

namespace Assets._Source.CodeBase.Meta.Services.Analytics.SoftDevKits
{
    internal class FirebaseAnalytic : IAnalyticProvider
    {
        public void LogGameOver(Dictionary<CustomEventNames, int> events)
        {
            Parameter[] parameters = Parse(events);
            FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLevelEnd, parameters);
        }

        public void LogLaserAttack()
        {
            FirebaseAnalytics.LogEvent(CustomEventNames.AttackLaserEvent.ToString());
        }

        public void LogStartGame()
        {
            FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLevelStart);
        }

        private Parameter[] Parse(Dictionary<CustomEventNames, int> events)
        {
            int index = 0;
            Parameter[] parameters = new Parameter[events.Count];

            foreach (var kvp in events)
            {
                parameters[index] = new Parameter(kvp.Key.ToString(), kvp.Value);
                index++;
            }

            return parameters;
        }
    }
}