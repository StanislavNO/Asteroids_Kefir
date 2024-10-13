using UnityEngine;

namespace Assets.Source.Code_base.Common
{
    public static class Utilities
    {
        public static Vector3 GetRandomEulerZ()
        {
            float minAngle = 0f;
            float maxAngle = 360f;

            float randomZ = Random.Range(minAngle, maxAngle);
            Vector3 randomVector = Vector3.forward * randomZ;

            return randomVector;
        }
    }
}
