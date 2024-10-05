using UnityEngine;

namespace Assets.Source.Code_base
{
    public class AttackPoint : MonoBehaviour
    {
        private Transform _transform;

        [field: SerializeField] public GameObject LaserBullet { get; private set; }

        public Quaternion Rotation => _transform.rotation;
        public Vector2 Position => _transform.position;

        private void Awake()
        {
            _transform = transform;
        }
    }
}