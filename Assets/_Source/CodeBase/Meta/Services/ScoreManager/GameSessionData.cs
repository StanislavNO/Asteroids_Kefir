using System;
using Newtonsoft.Json;

namespace Assets._Source.CodeBase.Core.Infrastructure.Services.Score
{
    [Serializable]
    public class GameSessionData
    {
        [JsonProperty]
        public int Score { get; private set; }

        public void Add(int reward)
        {
            if (reward < 0)
                throw new ArgumentOutOfRangeException(nameof(reward));

            Score += reward;
        }
    }
}