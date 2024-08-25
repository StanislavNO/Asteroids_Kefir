using Assets.Source.Code_base;
using System;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public event Action<Enemy> Died;

    [field: SerializeField] public EnemyNames Name { get; private set; }
    [field: SerializeField] public int Reward { get; private set; }
    [field: SerializeField] protected float Speed { get; private set; } = 2.5f;
    [field: SerializeField] protected Transform Transform { get; private set; }

    private void OnValidate()
    {
        if (Reward < 0)
            Reward = 0;

        if (Speed < 0)
            Speed = 0.1f;
    }

    private void OnDisable() => Died?.Invoke(this);

    private void FixedUpdate() => Move();

    public void TakeDamage() => gameObject.SetActive(false);

    protected abstract void Move();
}
