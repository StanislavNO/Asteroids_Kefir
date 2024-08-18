using System;
using System.Collections;
using UnityEngine;

namespace Assets.Source.Code_base
{
    public class TurbineViewController : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _leftTurbine; 
        [SerializeField] private SpriteRenderer _rightTurbine;
        [SerializeField] private SpriteRenderer _engine;

        private IInputService _input;
        private WaitForSecondsRealtime _turbineTimeWork;
        
        public void Init(IInputService inputService)
        {
            _input = inputService;

            _input.Rotating += ShowTurbine;
            _turbineTimeWork = new(1f);
        }

        private void Start()
        {
            _leftTurbine.enabled = false;
            _rightTurbine.enabled = false;
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
            _input.Rotating -= ShowTurbine;
        }

        private void ShowTurbine(float horizontalInput)
        {
            if (horizontalInput > 0)
                StartCoroutine(WriteRightTurbine());
            else
                StartCoroutine(WriteLeftTurbine());
        }

        private IEnumerator WriteLeftTurbine()
        {
            _leftTurbine.enabled = true;
            yield return _turbineTimeWork;
            _leftTurbine.enabled = false;
        }

        private IEnumerator WriteRightTurbine()
        {
            _rightTurbine.enabled = true;
            yield return _turbineTimeWork;
            _rightTurbine.enabled = false;
        }
    }
}