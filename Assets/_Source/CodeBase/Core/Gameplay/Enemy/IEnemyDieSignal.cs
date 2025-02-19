using System;

namespace Assets._Source.CodeBase.Core.Gameplay.Enemies
{
    public interface IEnemyDieSignal
    {
        event Action OnAsteroidDied;
        event Action OnUfoDied;
    }
}