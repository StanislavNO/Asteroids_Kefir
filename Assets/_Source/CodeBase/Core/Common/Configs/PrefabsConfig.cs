using Assets._Source.CodeBase.Core.Gameplay.BehaviourEffectors;
using Assets._Source.CodeBase.Core.Gameplay.Enemies;
using UnityEngine;

namespace Assets._Source.CodeBase.Core.Common.Configs
{
    [CreateAssetMenu(fileName = "PrefabsConfig", menuName = "PrefabsConfig")]
    public class PrefabsConfig : ScriptableObject
    {
        [field: SerializeField] public Character Player { get; private set; }
        [field: SerializeField] public EnemyPrefabsContainer EnemyPrefabs { get; private set; }
        [field: SerializeField] public Bullet DefaultBulletPrefab { get; private set; }
    }
}
