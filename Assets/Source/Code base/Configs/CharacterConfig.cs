using UnityEngine;

namespace Assets.Source.Code_base
{
    [CreateAssetMenu(fileName = "CharacterConfig", menuName = "Configs")]
    public class CharacterConfig : ScriptableObject
    {
        [field: SerializeField] public MovementConfig Movement {  get; private set; }
        [field: SerializeField] public WeaponConfig Weapon {get; private set;}
    }
}