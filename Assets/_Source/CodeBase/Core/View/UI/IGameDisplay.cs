using UnityEngine;

namespace Assets._Source.CodeBase.Core.View.UI
{
    public interface IGameDisplay
    {
        void ShowRotation(float angle);
        void ShowSpeed(float value);
        void ShowCoordinate(Vector2 position);
        void ShowLaserBullet(int count);

        void WriteWeaponCooldown(float duration);
    }
}