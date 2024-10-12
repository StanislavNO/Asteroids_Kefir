using Zenject;

namespace Assets.Source.Code_base
{
    public class CharacterViewController : IInitializable, IFixedTickable
    {
        private readonly IReadOnlyCharacter _character;
        private readonly IReadOnlyWeapon _weapon;
        private readonly IDisplay _display;

        public CharacterViewController(IDisplay display, IReadOnlyCharacter character, IReadOnlyWeapon weapon)
        {
            _character = character;
            _display = display;
        }

        public void Initialize()
        {
            UpdateView();
        }

        public void FixedTick()
        {
            UpdateView();
        }

        private void UpdateView()
        {
            _display.ShowCoordinate(_character.Stat.Position);
            _display.ShowSpeed(_character.Stat.Speed);
        }
    }
}