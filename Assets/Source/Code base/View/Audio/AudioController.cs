using UnityEngine;

namespace Assets.Source.Code_base
{
    public class AudioController : MonoBehaviour
    {
        [SerializeField] private AudioSource _bullet;
        [SerializeField] private AudioSource _laser;
        [SerializeField] private AudioSource _background;

        public void PlayBulletAttack() => _bullet.Play();

        public void PlayLaserAttack(float duration)
        {
            _background.Play();
        }
    }
}