using System;
using UnityEngine;

namespace Assets._Source.CodeBase.Core.Common.Configs
{
    [Serializable]
    public class WeaponConfig
    {
        [SerializeField][Range(0.1f, 5)] private float _laserCooldown;
        [SerializeField][Range(1, 5)] private int _laserBulletCount;
        [SerializeField][Range(1, 5)] private int _timeWorkLaser = 1;

        public int TimeWorkLaser => _timeWorkLaser;
        public float LaserCooldown => _laserCooldown;
        public int LaserBulletCount => _laserBulletCount;

        public void SetValues(float laserCooldown, int laserBulletCount, int timeWorkLaser)
        {
            _laserCooldown = laserCooldown;
            _laserBulletCount = laserBulletCount;
            _timeWorkLaser = timeWorkLaser;
        }
    }
}