using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Code_base
{
    public class ViewStatsPanelController : MonoBehaviour
    {
        private const string ANGLE = "”гол ";

        [SerializeField] private TMP_Text _speedometer;
        [SerializeField] private TMP_Text _compass;
        [SerializeField] private TMP_Text _coordinates;
        [SerializeField] private TMP_Text _laserBulletCount;
        [SerializeField] private Image _laserCooldown;

        private IReadOnlyCharacter _character;
        private float _laserStartCooldown;
        private float _laserCurrentCooldown;

        public void Init(IReadOnlyCharacter character)
        {
            if (character == null)
                throw new ArgumentNullException(nameof(character));

            _character = character;
            _laserStartCooldown = _character.Stat.Weapon.LaserCooldown;

            _character.Stat.Weapon.LaserCooldownChanged += OnLaserCooldown;
            _character.Stat.Weapon.LaserBulletChanged += OnLaserBulletChanged;
        }


        private void OnDestroy()
        {
            _character.Stat.Weapon.LaserCooldownChanged -= OnLaserCooldown;
            _character.Stat.Weapon.LaserBulletChanged -= OnLaserBulletChanged;
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
            int fullAngle = 360;
            int correctAngle = 180;
            float rotation = _character.Stat.RotationAngle;

            if (rotation > correctAngle)
                rotation -= fullAngle;

            _compass.SetText(ANGLE + rotation.ToString("F2"));
        }

        private void ShowSpeed() =>
            _speedometer.SetText(string.Format("{0:f1}", _character.Stat.Speed));

        private void ShowCoordinate() =>
            _coordinates.SetText(_character.Stat.Position.ToString());

        private void OnLaserCooldown(float time)
        {
            throw new NotImplementedException();
        }

        private void OnLaserBulletChanged(int countBullet)=>
            _laserBulletCount.SetText(countBullet.ToString());
    }
}