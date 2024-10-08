using UnityEngine;

namespace Assets.Source.Code_base
{
    [CreateAssetMenu(fileName ="PrefabsConfig" , menuName = "PrefabsConfig")]
    public class PrefabsConfig : ScriptableObject
    {
        [field: SerializeField] public Character Player {  get; private set; }
        [field: SerializeField] public EnemyPrefabsContainer EnemyPrefabs { get; private set; }
        [field: SerializeField] public Bullet DefaultBulletPrefab { get; private set; }
    }
}
