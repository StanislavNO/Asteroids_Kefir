using UnityEngine;

namespace Assets.Source.Code_base
{
    public class Asteroid : Enemy
    {
        public override void Accept(IEnemyVisitor visitor) =>
            visitor.Visit(this);

        protected override void Move() =>
            Transform.Translate(Vector2.up * Time.fixedDeltaTime * Speed);
    }
}
