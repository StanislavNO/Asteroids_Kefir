using System.Collections.Generic;
using UnityEngine;

namespace Assets._Source.CodeBase.Meta.View
{
    public class LeaderboardWindow : MonoBehaviour
    {
        [field: SerializeField] public int MaxLeaders { get; private set; }
        
        public LeaderView leaderPrefab;
        public Transform leaderboardContainer;

        public void Show(List<int> scores)
        {
            foreach (Transform child in leaderboardContainer)
                Destroy(child.gameObject);
            
            for (int i = 0; i < scores.Count; i++)
            {
                LeaderView entry = Instantiate(leaderPrefab, leaderboardContainer);
                entry.Score.text = $"{i + 1}. = {scores[i]} очков";
            }
        }
    }
}