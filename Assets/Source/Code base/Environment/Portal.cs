using UnityEngine;

namespace Assets.Source.Code_base
{
    public class Portal : MonoBehaviour
    {
        private Camera _camera;
        private Transform _transform;
        private Vector2 _screenSize;

        private float _leftBound;
        private float _rightBound;
        private float _bottomBound;
        private float _topBound;

        private void Awake()
        {
            _transform = GetComponent<Transform>();
            _camera = Camera.main;

            Vector3 bottomLeft = _camera.ViewportToWorldPoint(new Vector3(0, 0, _camera.nearClipPlane));
            Vector3 topRight = _camera.ViewportToWorldPoint(new Vector3(1, 1, _camera.nearClipPlane));

            _leftBound = bottomLeft.x;
            _rightBound = topRight.x;
            _bottomBound = bottomLeft.y;
            _topBound = topRight.y;
        }

        private void FixedUpdate()
        {
            if (_transform.position.x < _leftBound || _transform.position.x > _rightBound)
                _transform.position = new(-_transform.position.x, _transform.position.y);

            if (_transform.position.y < _bottomBound || _transform.position.y > _topBound)
                _transform.position = new(_transform.position.x, -_transform.position.y);
        }
    }
}