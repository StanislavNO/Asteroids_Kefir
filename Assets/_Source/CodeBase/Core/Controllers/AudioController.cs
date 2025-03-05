using Assets._Source.CodeBase.Core.Gameplay.Enemies;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets._Source.CodeBase.Core.Controllers
{
    public class AudioController : MonoBehaviour
    {
        [SerializeField] private AudioSource _bullet;
        [SerializeField] private AudioSource _laser;
        [SerializeField] private AudioSource _enemyDeath;

        private IAttackObserver _weapon;
        private EnemyLiveObserver _enemyLiveObserver;

        [Inject]
        private void Construct(IAttackObserver attacker, EnemyLiveObserver enemyLiveObserver)
        {
            _weapon = attacker;
            _enemyLiveObserver = enemyLiveObserver;
        }

        private void Awake()
        {
            _weapon.OnDefaultAttacked += OnBulletAttack;
            _weapon.OnLaserAttacking += OnLaserAttack;
            _enemyLiveObserver.OnDied += OnEnemyDied;
        }

        private void OnDestroy()
        {
            _weapon.OnDefaultAttacked -= OnBulletAttack;
            _weapon.OnLaserAttacking -= OnLaserAttack;
            _enemyLiveObserver.OnDied -= OnEnemyDied;
        }

        private void OnBulletAttack() => _bullet.Play();

        private void OnLaserAttack(float duration) =>
            PlayAsyncLaserAttack(duration);

        private void OnEnemyDied() => _enemyDeath.Play();

        private async void PlayAsyncLaserAttack(float duration)
        {
            int durationInMilliseconds = (int)duration * 1000;

            _laser.Play();
            await UniTask.Delay(durationInMilliseconds);
            _laser.Stop();
        }
    }
}