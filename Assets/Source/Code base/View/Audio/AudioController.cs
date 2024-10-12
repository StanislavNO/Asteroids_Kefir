using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Source.Code_base
{
    public class AudioController : MonoBehaviour
    {
        [SerializeField] private AudioSource _bullet;
        [SerializeField] private AudioSource _laser;
        [SerializeField] private AudioSource _background;

        public void PlayBulletAttack() => _bullet.Play();

        public async void PlayLaserAttack(float duration)
        {
            int durationInMilliseconds = (int)duration * 1000;

            _laser.Play();
            await UniTask.Delay(durationInMilliseconds);
            _laser.Stop();
        }
    }
}