using Assets._Source.CodeBase.Core.Gameplay.Enemies;
using System;
using System.Collections.Generic;
using Zenject;

namespace Assets._Source.CodeBase.Meta.Services.Analytics
{
    internal class AnalyticsController : IEventWriter
    {
        private readonly IAnalyticProvider _analyticProvider;
        private readonly EventCounter _eventCounter;

        private readonly Dictionary<CustomEventNames, int> _events;

        public AnalyticsController(
            IAnalyticProvider analyticProvider,
            EventCounter eventCounter)
        {
            _analyticProvider = analyticProvider;
            _eventCounter = eventCounter;

            _events = new Dictionary<CustomEventNames, int>()
            {
                { CustomEventNames.AttackDefaultCount, 0 },
                { CustomEventNames.AttackLaserCount, 0},
                { CustomEventNames.DeadAsteroidsCount, 0},
                { CustomEventNames.DeadUFOCount, 0}
            };
        }

        public void WriteGameOver()
        {
            SetValueForEvents();

            _analyticProvider.LogGameOver(_events);
        }

        public void WriteGameStart() =>
            _analyticProvider.LogStartGame();

        public void WriteAttackLaser()
        {
            _analyticProvider.LogLaserAttack();
            _eventCounter.WriteLaserAttack();
        }
            

        private void SetValueForEvents()
        {
            _events[CustomEventNames.AttackDefaultCount] = _eventCounter.DefaultAttack;
            _events[CustomEventNames.AttackLaserCount] = _eventCounter.LaserAttack;
            _events[CustomEventNames.DeadAsteroidsCount] = _eventCounter.DeadAsteroids;
            _events[CustomEventNames.DeadUFOCount] = _eventCounter.DeadUfo;
        }
    }
}