using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _reward;

    public event Action<int> Died;

    private void OnValidate()
    {
        if(_reward < 0)
            _reward = 0;
    }

    private void OnEnable()
    {
        Died?.Invoke(_reward);
    }

    public void TakeDamage()
    {
        gameObject.SetActive(false);
    }
}
