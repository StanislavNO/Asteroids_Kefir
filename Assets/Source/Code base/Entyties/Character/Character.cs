using Assets.Source.Code_base;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Character : MonoBehaviour, IReadOnlyCharacter
{
    private Rotator _rotator;
    private Mover _mover;
    private Weapon _weapon;
    private IInputService _input;

    public CharacterStats Stats { get; private set; }

    public void Init(IInputService input, IWeaponStat weaponStat)
    {
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();

        _input = input;
        Stats = new(rigidbody, transform, new Weapon(_input, weaponStat));

        _rotator = new(input, transform);
        _mover = new(input, rigidbody, transform);
    }

    private void OnDestroy()
    {
        _rotator.Destroy();
        _mover.Destroy();
    }

    private void Update()
    {
        Stats?.Update();
        _input?.Update();
    }
}
