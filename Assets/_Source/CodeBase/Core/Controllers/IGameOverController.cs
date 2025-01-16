namespace Assets._Source.CodeBase.Core.Controllers
{
    public interface IGameOverController
    {
        int ContinueCount { get; }
        void Continue();
        void GameOver();
    }
}