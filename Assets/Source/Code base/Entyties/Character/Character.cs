using Assets.Source.Code_base;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Character : MonoBehaviour, IReadOnlyCharacter, ICoroutineRunner
{
    [SerializeField] private CharacterConfig _characterConfig;
    [SerializeField] private AttackPoint _attackPoint;

    private Rotator _rotator;
    private Mover _mover;
    private Weapon _weapon;
    private IInputService _input;

    public CharacterStats Stat { get; private set; }

    public void Init(IInputService input)
    {
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();

        _input = input;
        _weapon = new Weapon(_input, _characterConfig.Weapon, this, _attackPoint);
        Stat = new(rigidbody, transform, _weapon);

        _rotator = new(input, transform, _characterConfig);
        _mover = new(input, rigidbody, transform, _characterConfig);
    }

    private void OnDestroy()
    {
        _rotator.Destroy();
        _mover.Destroy();
        _weapon.Destroy();
        StopAllCoroutines();
    }

    private void Update()
    {
        Stat?.Update();
        _input?.Update();
    }
}
