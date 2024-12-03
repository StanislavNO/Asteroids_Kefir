using Assets._source._code_base.Core.Controllers;
using Assets._source._code_base.Core.Infrastructure.Services.SceneSwitcher;
using Assets._source._code_base.Meta.Services.Analytics;
using Assets.Source.Code_base;
using Firebase.Analytics;
using System;
using Zenject;

namespace Assets._source._code_base.Meta
{
    internal class FirebaseAnalyticsController : IDisposable, IInitializable
    {
        private readonly IGameStartSignal _gameStartSignal;
        private readonly IGameOverSignal _gameOverSignal;
        private readonly IAttackObserver _attackObserver;
        private readonly PlayerEventCounter _playerEventCounter;

        public FirebaseAnalyticsController(
            IGameStartSignal gameStartSignal,
            IGameOverSignal gameOverSignal,
            IAttackObserver attackObserver,
            PlayerEventCounter playerEventCounter)
        {
            _gameStartSignal = gameStartSignal;
            _gameOverSignal = gameOverSignal;
            _attackObserver = attackObserver;
            _playerEventCounter = playerEventCounter;
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
        }

        private void OnGameOver()
        {
            Parameter[] parameters =
            {
                new(CustomEventNames.AttackDefaultCount.ToString(), _playerEventCounter.DefaultAttack),
                new(CustomEventNames.AttackLaserCount.ToString(), _playerEventCounter.LaserAttack),
                new(CustomEventNames.DeadAsteroidsCount.ToString(), _playerEventCounter.DeadAsteroids),
                new(CustomEventNames.DeadUFOCount.ToString(), _playerEventCounter.DeadUFO)
            };

            FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLevelEnd, parameters);
        }

        private void OnGameStarting()
        {
            FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLevelStart);
        }

        private void OnAttackLaser(float _)
        {
            FirebaseAnalytics.LogEvent(CustomEventNames.AttackLaserEvent.ToString());
        }
    }
}