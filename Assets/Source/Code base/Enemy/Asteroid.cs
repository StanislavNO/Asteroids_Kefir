using UnityEngine;

namespace Assets.Source.Code_base
{
    public class Asteroid : Enemy
    {
        protected override void Move() =>
            Transform.Translate(Vector2.up * Time.fixedDeltaTime * Speed);
    }
}
