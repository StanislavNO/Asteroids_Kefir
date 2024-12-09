using Assets._source._code_base.Core.Controllers;
using Assets._source._code_base.Core.Infrastructure.Services.SceneSwitcher;
using Assets._source._code_base.Meta.Services.Analytics;
using Assets.Source.Code_base;
using System;
using System.Collections.Generic;
using Zenject;

namespace Assets._source._code_base.Meta
{
    internal class AnalyticsController : IDisposable, IInitializable
    {
        private readonly IAnalyticProvider _analyticProvider;
        private readonly IGameStartSignal _gameStartSignal;
        private readonly IGameOverSignal _gameOverSignal;
        private readonly IAttackObserver _attackObserver;
        private readonly PlayerEventCounter _playerEventCounter;

        private readonly Dictionary<CustomEventNames, int> _events;

        public AnalyticsController(
            IAnalyticProvider analyticProvider,
            IGameStartSignal gameStartSignal,
            IGameOverSignal gameOverSignal,
            IAttackObserver attackObserver,
            PlayerEventCounter playerEventCounter)
        {
            _analyticProvider = analyticProvider;
            _gameStartSignal = gameStartSignal;
            _gameOverSignal = gameOverSignal;
            _attackObserver = attackObserver;
            _playerEventCounter = playerEventCounter;

            _events = new Dictionary<CustomEventNames, int>()
            {
                { CustomEventNames.AttackDefaultCount, 0 },
                { CustomEventNames.AttackLaserCount, 0},
                { CustomEventNames.DeadAsteroidsCount, 0},
                { CustomEventNames.DeadUFOCount, 0}
            };
        }

        public void Initialize()
        {
            _gameStartSignal.Starting += OnGameStarting;
            _gameOverSignal.GameOverring += OnGameOver;
            _attackObserver.LaserAttacking += OnAttackLaser;
        }

        public void Dispose()
        {
            _gameStartSignal.Starting -= OnGameStarting;
            _gameOverSignal.GameOverring -= OnGameOver;
            _attackObserver.LaserAttacking -= OnAttackLaser;
        }

        private void OnGameOver()
        {
            SetValueForEvents();

            _analyticProvider.LogGameOver(_events);
        }

        private void OnGameStarting() =>
            _analyticProvider.LogStartGame();

        private void OnAttackLaser(float _) =>
            _analyticProvider.LogLaserAttack();

        private void SetValueForEvents()
        {
            _events[CustomEventNames.AttackDefaultCount] = _playerEventCounter.DefaultAttack;
            _events[CustomEventNames.AttackLaserCount] = _playerEventCounter.LaserAttack;
            _events[CustomEventNames.DeadAsteroidsCount] = _playerEventCounter.DeadAsteroids;
            _events[CustomEventNames.DeadUFOCount] = _playerEventCounter.DeadUFO;
        }
    }
}