using System.Collections.Generic;
using UnityEngine;

namespace Assets._Source.CodeBase.Meta.View
{
    public class LeaderboardWindow : MonoBehaviour
    {
        [SerializeField] private LeaderView _leaderPrefab;
        [SerializeField] private Transform _leaderboardContainer;
             
        [field: SerializeField] public int MaxLeaders { get; private set; }

        public void Show(List<int> scores)
        {
            foreach (Transform child in _leaderboardContainer)
                Destroy(child.gameObject);
            
            for (int i = 0; i < scores.Count; i++)
            {
                LeaderView entry = Instantiate(_leaderPrefab, _leaderboardContainer);
                entry.Score.text = $"{i + 1}. = {scores[i]} очков";
            }
        }
    }
}