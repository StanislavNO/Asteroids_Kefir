using UnityEngine;

namespace Assets.Source.Code_base
{
    public interface IDisplay
    {
        void ShowRotation(float angle);
        void ShowSpeed(float value);
        void ShowCoordinate(Vector2 position);
        void ShowLaserBullet(int count);

        void WriteWeaponCooldown(float duration);
    }
}
