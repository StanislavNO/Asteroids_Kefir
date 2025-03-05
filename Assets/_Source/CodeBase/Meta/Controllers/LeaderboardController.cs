using System.Collections.Generic;
using System.Linq;
using _Source.CodeBase.Meta.Services.ScoreManager;
using Assets._Source.CodeBase.Core.Infrastructure.Services.Score;
using Assets._Source.CodeBase.Meta.View;
using Zenject;

namespace _Source.CodeBase.Meta.Controllers
{
    public class LeaderboardController : IInitializable
    {
        private readonly LeaderboardWindow _leaderboard;
        private readonly IScoreRepository _scoreRepository;

        private List<GameSessionData> _sessionData;
        private List<int> _topScores;

        public LeaderboardController(LeaderboardWindow leaderboard, IScoreRepository scoreRepository)
        {
            _leaderboard = leaderboard;
            _scoreRepository = scoreRepository;
            _sessionData = new List<GameSessionData>();
            _topScores = new List<int>();
        }

        public void Initialize()
        {
            InitScore();
            ShowScore();
        }

        private void ShowScore() => 
            _leaderboard.Show(_topScores);

        private void InitScore()
        {
            _sessionData = _scoreRepository.Load();
            _sessionData.Sort((a, b) => b.Score.CompareTo(a.Score));
            _topScores.Clear();
            
            if (_sessionData.Count > _leaderboard.MaxLeaders)
                SetItems();
            else
                InitEmptyItem();
        }

        private void SetItems()
        {
            _topScores = _sessionData
                .Take(_leaderboard.MaxLeaders)
                .Select(x => x.Score)
                .ToList();
        }

        private void InitEmptyItem()
        {
            for (int i = 0; i < _leaderboard.MaxLeaders; i++)
            {
                if (i < _sessionData.Count)
                    _topScores.Add(_sessionData[i].Score);
                else
                    _topScores.Add(0);
            }
        }
    }
}