namespace Assets.Source.Code_base
{
    public class PauseController : IReadOnlyPause
    {
        public bool IsPause { get; private set; } = false;

        public void Pause() => IsPause = true;

        public void Play() => IsPause = false;
    }
}