using System.Collections.Generic;
using _Source.CodeBase.Meta.Services.ScoreManager;
using Assets._Source.CodeBase.Meta.View;
using Zenject;

namespace _Source.CodeBase.Meta.Controllers
{
    public class LeaderboardController : IInitializable
    {
        private readonly LeaderboardWindow _leaderboard;
        private readonly IScoreRepositoryController _scoreRepository;

        private List<int> _scores;
        private List<int> _topScores;

        public LeaderboardController(LeaderboardWindow leaderboard, IScoreRepositoryController scoreRepository)
        {
            _leaderboard = leaderboard;
            _scoreRepository = scoreRepository;
            _scores = new List<int>();
            _topScores = new List<int>();
        }

        public void Initialize()
        {
            InitScore();
            ShowScore();
        }

        private void ShowScore()
        {
            _leaderboard.Show(_topScores);
        }

        private void InitScore()
        {
            _scores = _scoreRepository.Load();
            _scores.Sort((a, b) => b.CompareTo(a)); 

            if (_scores.Count > _leaderboard.MaxLeaders)
                _topScores = _scores.GetRange(0, _leaderboard.MaxLeaders);
            else
                InitEmptyItem();
        }

        private void InitEmptyItem()
        {
            for (int i = 0; i < _leaderboard.MaxLeaders; i++)
            {
                if (i < _scores.Count)
                    _topScores.Add(_scores[i]);
                else
                    _topScores.Add(0);
            }
        }
    }
}