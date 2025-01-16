using UnityEngine;

namespace Assets._Source.CodeBase.Core.Gameplay.Enemies
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