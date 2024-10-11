namespace Assets.Source.Code_base.Gameplay.Character
{
    public interface IMovement
    {
        void Move(float verticalAxis);
        void Rotate(float horizontalAxis);
    }
}
