using Assets._Source.CodeBase.Core.Gameplay.Enemies;
using System;
using UnityEngine;

namespace Assets._Source.CodeBase.Core.Common.Configs
{
    [Serializable]
    public class EnemyPrefabsContainer
    {
        [field: SerializeField] public MiniAsteroid AsteroidMini { get; private set; }
        [field: SerializeField] public Asteroid AsteroidBig { get; private set; }
        [field: SerializeField] public CharacterFollower Ufo { get; private set; }
    }
}
