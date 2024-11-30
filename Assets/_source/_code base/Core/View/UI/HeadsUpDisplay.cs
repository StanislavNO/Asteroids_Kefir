using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

        public void WriteWeaponCooldown(float duration) =>
            StartCoroutine(WriteCooldown(duration));

        private IEnumerator WriteCooldown(float duration)
        {
            float startValue = 0;
            float counterTime = 0;
            _laserCooldownImage.fillAmount = startValue;

            while (counterTime < duration)
            {
                counterTime += Time.deltaTime;
                _laserCooldownImage.fillAmount = (counterTime / duration);
                yield return null;
            }
        }
    }
}