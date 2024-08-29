using System.Collections.Generic;

namespace Assets.Source.Code_base
{
    public class BulletPool : ObjectPool<Bullet>
    {
        private readonly List<Bullet> _activeBullets;

        public void OnDestroy()
        {
            if (_activeBullets.Count > 0)
            {
                foreach (Bullet bullet in _activeBullets)
                    bullet.AttackComplied -= OnBulletDeactivated;
            }
        }

        public override bool TryGet(out Bullet obj)
        {
            if (base.TryGet(out obj))
            {
                _activeBullets.Add(obj);
                obj.AttackComplied += OnBulletDeactivated;

                return true;
            }

            return false;
        }

        private void OnBulletDeactivated(Bullet bullet)
        {
            bullet.AttackComplied -= OnBulletDeactivated;
            _activeBullets.Remove(bullet);
        }
    }
}