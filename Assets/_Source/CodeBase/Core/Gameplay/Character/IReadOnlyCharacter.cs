using System;

namespace Assets._Source.CodeBase.Core.Gameplay.BehaviourEffectors
{
    public interface IReadOnlyCharacter
    {
        event Action OnDied;
        CharacterStats Stat { get; }
    }
}
