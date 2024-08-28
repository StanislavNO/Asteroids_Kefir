using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Code_base
{
    public class ViewController : MonoBehaviour
    {
        private const string ANGLE = "”гол ";

        [SerializeField] private Canvas _gameOverPanel;
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private TMP_Text _speedometer;
        [SerializeField] private TMP_Text _compass;
        [SerializeField] private TMP_Text _coordinates;
        [SerializeField] private TMP_Text _laserBulletCount;
        [SerializeField] private Image _laserCooldownImage;

        private IReadOnlyCharacter _character;
        private float _laserCooldown;
        private float _laserCurrentCooldown;

        public void Init(IReadOnlyCharacter character)
        {
            _character = character;
            _laserCooldown = _character.Stat.Weapon.LaserCooldown;
            OnLaserBulletChanged(_character.Stat.Weapon.LaserBulletCount);

            _character.Stat.Weapon.LaserRecharging += OnLaserCooldown;
            _character.Stat.Weapon.LaserBulletChanged += OnLaserBulletChanged;
        }

        [field: SerializeField] public Button RestartButton { get; private set; }

        private void OnDestroy()
        {
            _character.Stat.Weapon.LaserRecharging -= OnLaserCooldown;
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

        public void ShowScore(int value) =>
            _scoreText.SetText(value.ToString());

        public void ShowGameOverPanel() =>
            _gameOverPanel.gameObject.SetActive(true);

        public void HideGameOverPanel() =>
            _gameOverPanel.gameObject.SetActive(false);

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

        private void OnLaserCooldown() =>
            StartCoroutine(ShowCooldown());

        private void OnLaserBulletChanged(int countBullet) =>
            _laserBulletCount.SetText(countBullet.ToString());

        private IEnumerator ShowCooldown()
        {
            float startValue = 0;
            float counterTime = 0;
            _laserCooldownImage.fillAmount = startValue;

            while (counterTime < _laserCooldown)
            {
                counterTime += Time.deltaTime;
                _laserCooldownImage.fillAmount = (counterTime / _laserCooldown);
                yield return null;
            }
        }
    }
}