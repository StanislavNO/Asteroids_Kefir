using UnityEngine;

namespace Assets._Source.CodeBase.Core.Common
{
    public class SpawnPointMarker : MonoBehaviour
    {
        [SerializeField] private Transform _transform;

        public Vector3 Position => _transform.position;
    }
}