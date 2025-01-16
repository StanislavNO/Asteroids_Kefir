using Assets._Source.CodeBase.Core.Gameplay.Enemies;
using System;
using UnityEngine;
using Zenject;

namespace Assets._Source.CodeBase.Core.Gameplay.BehaviourEffectors
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Character : MonoBehaviour, IReadOnlyCharacter, ICharacter
    {
        public event Action Die;

        [SerializeField] private AttackPoint _attackPoint;

        public CharacterStats Stat { get; private set; }
        public Transform Transform { get; private set; }
        public Rigidbody2D Rigidbody { get; private set; }

        [Inject]
        public void Construct(IWeaponInitializator weapon)
        {
            Transform = transform;
            Rigidbody = GetComponent<Rigidbody2D>();

            weapon.Init(_attackPoint);
            Stat = new(this);
        }

        private void Update()
        {
            Stat.Update();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Enemy _))
                Die?.Invoke();
        }
    }
}