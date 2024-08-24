using System.Collections;
using UnityEngine;

namespace Assets.Source.Code_base
{
    [RequireComponent(typeof(Collider2D))]
    public class Laser : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Enemy enemy))
                enemy.TakeDamage();
        }
    }
}