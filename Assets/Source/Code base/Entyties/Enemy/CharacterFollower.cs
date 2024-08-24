using UnityEngine;

namespace Assets.Source.Code_base
{
    public class CharacterFollower : MonoBehaviour
    {
        [SerializeField][Range(0.1f, 5f)] private float _speed = 2.5f;

        private Transform _character;
        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        private void Update()
        {
            if (_character is not null)
                MoveToCharacter();
        }

        public void SetTarget(Transform target) => _character = target;

        private void MoveToCharacter()
        {
            _transform.position = Vector3.MoveTowards(
                            _transform.position,
                            _character.position,
                            _speed * Time.deltaTime);
        }
    }
}