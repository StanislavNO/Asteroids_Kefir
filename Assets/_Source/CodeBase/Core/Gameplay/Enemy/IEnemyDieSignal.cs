using System;

namespace Assets._Source.CodeBase.Core.Gameplay.Enemies
{
    public interface IEnemyDieSignal
    {
        event Action AsteroidDie;
        event Action UFODie;
    }
}