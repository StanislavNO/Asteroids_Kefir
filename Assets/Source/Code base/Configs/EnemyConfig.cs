using UnityEngine;

namespace Assets.Source.Code_base
{
    [CreateAssetMenu(fileName ="EnemyConfig" , menuName = "EnemyConfig")]
    public class EnemyConfig : ScriptableObject
    {
        [field: SerializeField] public EnemyPrefabsContainer Prefabs { get; private set; }
    }
}
