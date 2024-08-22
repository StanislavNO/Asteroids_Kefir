using UnityEngine;

namespace Assets.Source.Code_base
{
    public class AttackPoint : MonoBehaviour
    {
        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        public Vector2 AttackVector => _transform.rotation.eulerAngles;
    }
}