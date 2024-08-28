using System;
using UnityEngine;

namespace Assets.Source.Code_base
{
    [Serializable]
    public class EnemyPrefabsContainer
    {
        [field: SerializeField] public Enemy AsteroidMini { get; private set; }
        [field: SerializeField] public Enemy AsteroidBig { get; private set; }
        [field: SerializeField] public Enemy Ufo { get; private set; }
    }
}
