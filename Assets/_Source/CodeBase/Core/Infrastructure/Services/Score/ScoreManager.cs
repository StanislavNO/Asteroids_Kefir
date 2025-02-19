using System;

namespace Assets._Source.CodeBase.Core.Infrastructure.Services.Score
{
    public class ScoreManager : IReadOnlyScore
    {
        public int Points { get; private set; } = 0;

        public void Add(int reward)
        {
            if (reward < 0)
                throw new ArgumentOutOfRangeException(nameof(reward));

            Points += reward;
        }

        public void Clear() => Points = 0;
    }
}