using System;
using UnityEngine;

namespace Assets.Source.Code_base
{
    [Serializable]
    public class WeaponConfig
    {
        [SerializeField][Range(0.1f,5)] private float _laserCooldown;
        [SerializeField][Range(1,5)] private int _laserBulletCount;

        public int TimeWorkLaser => 1;
        public float LaserCooldown => _laserCooldown;
        public int LaserBulletCount => _laserBulletCount;
    }
}