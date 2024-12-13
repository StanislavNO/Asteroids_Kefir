using UnityEngine;

namespace Assets.Source.Code_base
{
    [CreateAssetMenu(fileName = "CharacterConfig", menuName = "CharacterConfig")]
    public class CharacterConfig : ScriptableObject
    {
        [field: SerializeField] public MovementConfig Movement { get; private set; }
        [field: SerializeField] public WeaponConfig Weapon { get; private set; }

        public void SetConfigs(MovementConfig MovementConfig, WeaponConfig WeaponConfig)
        {
            Movement = MovementConfig;
            Weapon = WeaponConfig;
        }
    }
}