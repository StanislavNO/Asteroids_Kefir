using System;

namespace Assets.Source.Code_base
{
    public class ScoreManager
    {
        public ScoreManager()
        {
            Score = 0;
        }

        public int Score { get; private set; }

        public void Add(int reward)
        {
            if (reward < 0)
                throw new ArgumentOutOfRangeException(nameof(reward));

            Score += reward;
        }

        public void Clear() => Score = 0;
    }
}
