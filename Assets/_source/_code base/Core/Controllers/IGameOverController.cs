namespace Assets._source._code_base.Core.Controllers
{
    public interface IGameOverController
    {
        int ContinueCount { get; }
        void Continue();
        void GameOver();
    }
}