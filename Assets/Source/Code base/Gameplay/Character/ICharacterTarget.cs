using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Source.Code_base.Gameplay.Character
{
    public interface ICharacterTarget
    {
        Transform Transform { get; }
    }
}
