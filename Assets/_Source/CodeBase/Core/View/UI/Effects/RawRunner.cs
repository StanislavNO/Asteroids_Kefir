using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Source.CodeBase.Core.View.UI.Effects
{
    public class RawRunner : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private RawImage _image;

        private void Start()
        {
            StartCoroutine(RunImage());
        }

        private IEnumerator RunImage()
        {
            while (enabled)
            {
                Rect uvRect = _image.uvRect;
                uvRect.x += _speed * Time.deltaTime;
                _image.uvRect = uvRect;

                yield return null;
            }
        }
    }
}