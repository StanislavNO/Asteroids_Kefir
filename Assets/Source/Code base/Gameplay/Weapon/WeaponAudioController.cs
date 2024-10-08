using UnityEngine;

namespace Assets.Source.Code_base
{
    public class WeaponAudioController : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;

        private IWeapon _weapon;

        public void Init(IWeapon weapon)
        {
            _weapon = weapon;
            Debug.Log("Weapon Init" + _weapon == null);
        }

        private void Start()
        {
            _weapon.Attacking += OnPlayAttack;
        }

        private void OnDestroy()
        {
            _weapon.Attacking -= OnPlayAttack;
        }

        public void OnPlayAttack() => _audioSource.Play();
    }
}