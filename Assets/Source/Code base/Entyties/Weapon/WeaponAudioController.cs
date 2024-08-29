using UnityEngine;

namespace Assets.Source.Code_base
{
    public class WeaponAudioController : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;

        public void PlayAttack() => _audioSource.Play();
    }
}