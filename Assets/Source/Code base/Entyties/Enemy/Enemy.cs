using System;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private int _reward;
    [SerializeField][Range(0.1f, 5f)] private float _speed = 2.5f;

    public event Action<int> Died;

    [field: SerializeField] protected Transform Transform { get; private set; }
    protected float Speed => _speed;

    private void OnValidate()
    {
        if(_reward < 0)
            _reward = 0;
    }

    private void OnEnable()
    {
        Died?.Invoke(_reward);
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void TakeDamage()
    {
        gameObject.SetActive(false);
    }

    protected abstract void Move();
}
