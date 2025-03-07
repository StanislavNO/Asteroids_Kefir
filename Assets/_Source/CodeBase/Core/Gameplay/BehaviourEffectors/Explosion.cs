using UnityEngine;

namespace _Source.CodeBase.Core.Gameplay.BehaviourEffectors
{
    public class Explosion : MonoBehaviour
    {
        [field: SerializeField] public Animator Animator { get; private set; }
        public WaitForSeconds Durration { get; private set; }

        private void Awake() =>
            Durration = new(Animator.GetCurrentAnimatorStateInfo(0).length);
    }
}