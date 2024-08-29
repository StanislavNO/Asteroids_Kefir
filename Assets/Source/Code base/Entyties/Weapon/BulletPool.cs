using System.Collections.Generic;

namespace Assets.Source.Code_base
{
    public class BulletPool : IPool
    {
        private readonly Queue<Bullet> _bullets;
        private readonly BulletFactory _bulletFactory;
        private readonly AttackPoint _attackPoint;

        public BulletPool(WeaponConfig weaponStat, AttackPoint attackPoint)
        {
            //_bulletFactory = new(weaponStat, attackPoint, this);
            _attackPoint = attackPoint;
            _bullets = new Queue<Bullet>();
        }

        public Bullet Get()
        {
            if (_bullets.Count == 0)
                return _bulletFactory.Create();

            Bullet bullet = _bullets.Dequeue();
            bullet.gameObject.SetActive(true);
            bullet.transform.position = _attackPoint.Position;
            bullet.transform.rotation = _attackPoint.Rotation;

            return bullet;
        }

        public void Put(Bullet bullet) => _bullets.Enqueue(bullet);
    }
}