using Assets._Source.CodeBase.Core.Gameplay.Enemies;
using System;
using Zenject;

namespace Assets._Source.CodeBase.Meta.Services.Analytics
{
    internal class PlayerEventCounter : IInitializable, IDisposable
    {
        private readonly IAttackObserver _attackObserver;
        private readonly IEnemyDieSignal _enemyDieSignal;

        public int DefaultAttack { get; private set; } = 0;
        public int LaserAttack { get; private set; } = 0;
        public int DeadAsteroids { get; private set; } = 0;
        public int DeadUFO { get; private set; } = 0;

        public PlayerEventCounter(IAttackObserver attackObserver, IEnemyDieSignal enemyDieSignal)
        {
            _attackObserver = attackObserver;
            _enemyDieSignal = enemyDieSignal;
        }

        public void Initialize()
        {
            _attackObserver.OnDefaultAttacked += OnDefaultAttack;
            _attackObserver.OnLaserAttacking += OnLaserAttack;
            _enemyDieSignal.OnUfoDied += OnOnUfoDied;
            _enemyDieSignal.OnAsteroidDied += OnOnAsteroidDied;
        }

        public void Dispose()
        {
            _attackObserver.OnDefaultAttacked -= OnDefaultAttack;
            _attackObserver.OnLaserAttacking -= OnLaserAttack;
            _enemyDieSignal.OnUfoDied -= OnOnUfoDied;
            _enemyDieSignal.OnAsteroidDied -= OnOnAsteroidDied;
        }

        private void OnOnAsteroidDied() =>
            DeadAsteroids++;

        private void OnOnUfoDied() =>
            DeadUFO++;

        private void OnLaserAttack(float _) =>
            LaserAttack++;

        private void OnDefaultAttack() =>
            DefaultAttack++;
    }
}