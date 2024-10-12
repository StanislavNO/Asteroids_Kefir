using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Source.Code_base
{
    public class HeadsUpDisplay : MonoBehaviour, IDisplay
    {
        private const string ANGLE = "Угол ";

        [SerializeField] private TMP_Text _speedometer;
        [SerializeField] private TMP_Text _compass;
        [SerializeField] private TMP_Text _coordinates;
        [SerializeField] private TMP_Text _laserBulletCount;
        [SerializeField] private Image _laserCooldownImage;

        private IReadOnlyWeapon _weapon;

        [Inject]
        private void Construct(IReadOnlyWeapon weapon)
        {
            _weapon = weapon;
        }

        private void OnEnable()
        {
            _weapon.LaserRecharging += OnLaserCooldown;
            _weapon.LaserBulletChanged += ShowLaserBullet;
        }

        private void OnDisable()
        {
            _weapon.LaserRecharging -= OnLaserCooldown;
            _weapon.LaserBulletChanged -= ShowLaserBullet;
        }

        public void ShowRotation(float angle)
        {
            int fullAngle = 360;
            int correctAngle = 180;
            float rotation = angle;

            if (rotation > correctAngle)
                rotation -= fullAngle;

            _compass.SetText(ANGLE + rotation.ToString("F2"));
        }

        public void ShowSpeed(float value) =>
            _speedometer.SetText(string.Format("{0:f1}", value));

        public void ShowCoordinate(Vector2 position) =>
            _coordinates.SetText(position.ToString());

        public void ShowLaserBullet(int count) =>
            _laserBulletCount.SetText(count.ToString());

        private void OnLaserCooldown(float value) =>
            StartCoroutine(ShowCooldown(value));

        private IEnumerator ShowCooldown(float value)
        {
            float startValue = 0;
            float counterTime = 0;
            _laserCooldownImage.fillAmount = startValue;

            while (counterTime < value)
            {
                counterTime += Time.deltaTime;
                _laserCooldownImage.fillAmount = (counterTime / value);
                yield return null;
            }
        }
    }
}