using System;

namespace Assets.Source.Code_base
{
    public interface IEnemyDieSignal
    {
        event Action AsteroidDie;
        event Action UFODie;
    }
}