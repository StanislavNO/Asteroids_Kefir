using System;

namespace Assets.Source.Code_base
{
    public interface IReadOnlyCharacter
    {
        event Action Die;
        CharacterStats Stat { get; }
    }
}
