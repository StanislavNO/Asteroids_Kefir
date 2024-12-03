using Assets.Source.Code_base;
using System;
using Zenject;

namespace Assets._source._code_base.Meta.Services.Analytics
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
            _attackObserver.DefaultAttacking += OnDefaultAttack;
            _attackObserver.LaserAttacking += OnLaserAttack;
            _enemyDieSignal.UFODie += OnUfoDie;
            _enemyDieSignal.AsteroidDie += OnAsteroidDie;
        }

        public void Dispose()
        {
            _attackObserver.DefaultAttacking -= OnDefaultAttack;
            _attackObserver.LaserAttacking -= OnLaserAttack;
            _enemyDieSignal.UFODie -= OnUfoDie;
            _enemyDieSignal.AsteroidDie -= OnAsteroidDie;
        }

        private void OnAsteroidDie()=>
            DeadAsteroids++;

        private void OnUfoDie() =>
            DeadUFO++;

        private void OnLaserAttack(float _) =>
            LaserAttack++;

        private void OnDefaultAttack() =>
            DefaultAttack++;
    }
}