using UnityEngine;

namespace Assets.Source.Code_base
{
    public abstract class Attacker : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Enemy enemy))
                enemy.TakeDamage();

            Attack();
        }

        protected virtual void Attack() { }
    }
}
