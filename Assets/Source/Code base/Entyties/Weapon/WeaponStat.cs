using System;
using UnityEngine;

namespace Assets.Source.Code_base
{
    [Serializable]
    public class WeaponStat
    {
        [field: SerializeField] public Bullet LaserBulletPrefab {get; private set; }
    }
}