using System.Collections.Generic;

namespace _Source.CodeBase.Meta.Services.ScoreManager
{
    public interface IScoreRepositoryController
    {
        void Save(int score);
        List<int> Load();
    }
}