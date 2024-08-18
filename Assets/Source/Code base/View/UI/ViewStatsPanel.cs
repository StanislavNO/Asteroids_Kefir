using System;
using TMPro;
using UnityEngine;

namespace Assets.Source.Code_base
{
    public class ViewStatsPanel : MonoBehaviour
    {
        private const string ANGLE = "”гол ";

        [SerializeField] private TMP_Text _speedometer;
        [SerializeField] private TMP_Text _compass;
        [SerializeField] private TMP_Text _coordinates;

        private IReadOnlyCharacter _character;

        public void Init(IReadOnlyCharacter character)
        {
            if (character == null)
                throw new ArgumentNullException(nameof(character));

            _character = character;
        }

        private void FixedUpdate()
        {
            if (_character is not null)
            {
                ShowSpeed();
                ShowRotation();
                ShowCoordinate();
            }
        }

        private void ShowRotation()
        {
            int correctAngle = 180;
            int fullAngle = 360;
            float rotation = _character.Stats.RotationAngle;

            if (rotation > correctAngle)
                rotation -= fullAngle;

            _compass.SetText(ANGLE + rotation.ToString("F2"));
        }

        private void ShowSpeed() =>
            _speedometer.SetText(string.Format("{0:f1}", _character.Stats.Speed));

        private void ShowCoordinate() =>
            _coordinates.SetText(_character.Stats.Position.ToString());
    }
}