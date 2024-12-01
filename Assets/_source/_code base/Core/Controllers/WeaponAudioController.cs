using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Source.Code_base
{
    public class WeaponAudioController : MonoBehaviour
    {
        [SerializeField] private AudioSource _bullet;
        [SerializeField] private AudioSource _laser;

        private IAttackObserver _weapon;

        [Inject]
        private void Construct(IAttackObserver attacker)
        {
            _weapon = attacker;
        }

        private void OnEnable()
        {
            _weapon.DefaultAttacking += OnBulletAttack;
            _weapon.LaserAttacking += OnLaserAttack;
        }

        private void OnDisable()
        {
            _weapon.DefaultAttacking -= OnBulletAttack;
            _weapon.LaserAttacking -= OnLaserAttack;
        }

        public void OnBulletAttack() =>
            _bullet.Play();

        public void OnLaserAttack(float duration) =>
            PlayAsynkLaserAttack(duration);

        private async void PlayAsynkLaserAttack(float duration)
        {
            int durationInMilliseconds = (int)duration * 1000;

            _laser.Play();
            await UniTask.Delay(durationInMilliseconds);
            _laser.Stop();
        }
    }
}