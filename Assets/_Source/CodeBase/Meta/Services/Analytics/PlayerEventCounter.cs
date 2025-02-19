using Assets._Source.CodeBase.Core.Gameplay.Enemies;
using System;
using Zenject;

namespace Assets._Source.CodeBase.Meta.Services.Analytics
{
    internal class PlayerEventCounter : IInitializable, IDisposable
    {
        private readonly IAttackObserver _attackObserver;
        private readonly IEnemyDeadSignal _enemyDeadSignal;

        public int DefaultAttack { get; private set; } = 0;
        public int LaserAttack { get; private set; } = 0;
        public int DeadAsteroids { get; private set; } = 0;
        public int DeadUfo { get; private set; } = 0;

        public PlayerEventCounter(IAttackObserver attackObserver, IEnemyDeadSignal enemyDeadSignal)
        {
            _attackObserver = attackObserver;
            _enemyDeadSignal = enemyDeadSignal;
        }

        public void Initialize()
        {
            _attackObserver.OnDefaultAttacked += OnDefaultAttack;
            _attackObserver.OnLaserAttacking += OnLaserAttack;
            _enemyDeadSignal.OnUfoDied += OnOnUfoDied;
            _enemyDeadSignal.OnAsteroidDied += OnOnAsteroidDied;
        }

        public void Dispose()
        {
            _attackObserver.OnDefaultAttacked -= OnDefaultAttack;
            _attackObserver.OnLaserAttacking -= OnLaserAttack;
            _enemyDeadSignal.OnUfoDied -= OnOnUfoDied;
            _enemyDeadSignal.OnAsteroidDied -= OnOnAsteroidDied;
        }

        private void OnOnAsteroidDied() =>
            DeadAsteroids++;

        private void OnOnUfoDied() =>
            DeadUfo++;

        private void OnLaserAttack(float _) =>
            LaserAttack++;

        private void OnDefaultAttack() =>
            DefaultAttack++;
    }
}