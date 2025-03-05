using System.Collections.Generic;
using Assets._Source.CodeBase.Core.Infrastructure.Services.Score;

namespace _Source.CodeBase.Meta.Services.ScoreManager
{
    public interface IScoreRepository
    {
        void Save(GameSessionData data);
        List<GameSessionData> Load();
    }
}