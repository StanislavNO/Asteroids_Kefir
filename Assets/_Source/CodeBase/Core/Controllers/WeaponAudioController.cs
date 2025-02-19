using Assets._Source.CodeBase.Core.Gameplay.Enemies;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets._Source.CodeBase.Core.Controllers
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

        private void Awake()
        {
            _weapon.OnDefaultAttacked += OnBulletAttack;
            _weapon.OnLaserAttacking += OnLaserAttack;
        }

        private void OnDestroy()
        {
            _weapon.OnDefaultAttacked -= OnBulletAttack;
            _weapon.OnLaserAttacking -= OnLaserAttack;
        }

        public void OnBulletAttack() =>
            _bullet.Play();

        public void OnLaserAttack(float duration) =>
            PlayAsyncLaserAttack(duration);

        private async void PlayAsyncLaserAttack(float duration)
        {
            int durationInMilliseconds = (int)duration * 1000;

            _laser.Play();
            await UniTask.Delay(durationInMilliseconds);
            _laser.Stop();
        }
    }
}