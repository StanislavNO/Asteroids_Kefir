using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Source.Code_base.Gameplay.Character
{
    public interface IMovement
    {
        void Move(float verticalAxis);
        void Rotate(float horezontalAxis);
    }
}
