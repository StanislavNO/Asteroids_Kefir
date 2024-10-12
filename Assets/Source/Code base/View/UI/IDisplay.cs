using UnityEngine;

namespace Assets.Source.Code_base
{
    public interface IDisplay
    {
        void ShowRotation(float angle);
        void ShowSpeed(float value);
        void ShowCoordinate(Vector3 position);
        void ShowLaserCooldown(float value);
        void ShowLaserBullet(int count);
    }
}
