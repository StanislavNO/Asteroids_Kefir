namespace Assets._Source.CodeBase.Core.Infrastructure.Services.TimeManager
{
    public class PauseController : IReadOnlyPause
    {
        public bool IsPause { get; private set; } = false;

        public void Pause() => IsPause = true;

        public void Play() => IsPause = false;
    }
}