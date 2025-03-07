using System;
using System.Collections;
using _Source.CodeBase.Core.Gameplay.BehaviourEffectors;
using Assets._Source.CodeBase.Core.Common.Configs;
using Assets._Source.CodeBase.Core.Infrastructure.Services.Input;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Assets._Source.CodeBase.Core.Gameplay.Enemies
{
    public class ExplosionSpawner : IInitializable, IDisposable
    {
        private readonly Explosion _effect;
        private readonly EnemyLiveObserver _enemyLiveObserver;
        private readonly Pool<Explosion> _pool;
        private readonly Transform _parent;

        public ExplosionSpawner(PrefabsConfig prefabsConfig, EnemyLiveObserver enemyLiveObserver)
        {
            _effect = prefabsConfig.ExplosionEffect;
            _enemyLiveObserver = enemyLiveObserver;
            _pool = new(Create);
            _parent = new GameObject("ExplosionPool").transform;
        }

        public void Initialize() => 
            _enemyLiveObserver.OnDied += OnEnemyDied;
        
        public void Dispose() => 
            _enemyLiveObserver.OnDied -= OnEnemyDied;

        private Explosion Create() => Object.Instantiate(_effect, _parent);

        private void OnEnemyDied(Vector2 position)
        {
            Explosion explosion = _pool.Get();
            explosion.transform.position = position;
            explosion.StartCoroutine(DisableAfterAnimation(explosion));
        }

        private IEnumerator DisableAfterAnimation(Explosion explosion)
        {
            yield return explosion.Durration;
            _pool.Put(explosion);
        }
    }
}