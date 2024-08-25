using UnityEngine;

namespace Assets.Source.Code_base
{
    [RequireComponent(typeof(Collider))]
    public class Bullet : Attacker
    {
        [SerializeField] private float _speed;

        private Transform _transform;
        private IPool _pool;

        public void Init(IPool pool)
        {
            _pool = pool;
        }

        private void Awake()
        {
            _transform = transform;
        }

        private void Update()
        {
            _transform.Translate(Vector2.up * _speed * Time.deltaTime);
        }

        protected override void AttackComplied()
        {
            base.AttackComplied();

            gameObject.SetActive(false);
            _pool.Put(this);
        }
    }
}