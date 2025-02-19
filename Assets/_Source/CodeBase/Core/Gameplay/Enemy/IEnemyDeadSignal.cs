using System;

namespace Assets._Source.CodeBase.Core.Gameplay.Enemies
{
    public interface IEnemyDeadSignal
    {
        event Action OnAsteroidDied;
        event Action OnUfoDied;
    }
}