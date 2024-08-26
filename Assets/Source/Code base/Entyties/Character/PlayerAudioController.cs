using UnityEngine;

namespace Assets.Source.Code_base
{
    public class PlayerAudioController : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;

        private IInputService _input;

        public void Init(IInputService input)
        {
            _input = input;
            _input.DefoldAttacking += PlayAttack;
        }

        private void OnDestroy() =>
            _input.DefoldAttacking -= PlayAttack;

        private void PlayAttack() => _audioSource.Play();
    }
}