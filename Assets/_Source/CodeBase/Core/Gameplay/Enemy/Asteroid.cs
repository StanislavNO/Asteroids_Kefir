using UnityEngine;

namespace Assets._Source.CodeBase.Core.Gameplay.Enemies
{
    public class Asteroid : Enemy
    {
        protected override void Move() =>
            Transform.Translate(Vector2.up * Time.fixedDeltaTime * Speed);
    }
}
