using Assets.Source.Code_base;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Character : MonoBehaviour, IReadOnlyCharacter
{
    private Rotator _rotator;
    private Mover _mover;
    private IInputService _input;

    public void Init(IInputService input)
    {
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();

        _input = input;
        Stats = new(rigidbody, transform, new Weapon());

        _rotator = new(input, transform);
        _mover = new(input, rigidbody, transform);
    }

    public CharacterStats Stats { get; private set; }

    private void OnDestroy()
    {
        _rotator.Destroy();
    }

    private void Update()
    {
        Stats?.Update();
        _input?.Tick();
    }
}
