using System;
using UnityEngine;

namespace Assets.Source.Code_base
{
    [Serializable]
    public class EnemyPrefabsContainer
    {
        [field: SerializeField] public MiniAsteroid AsteroidMini { get; private set; }
        [field: SerializeField] public Asteroid AsteroidBig { get; private set; }
        [field: SerializeField] public CharacterFollower Ufo { get; private set; }
    }
}
