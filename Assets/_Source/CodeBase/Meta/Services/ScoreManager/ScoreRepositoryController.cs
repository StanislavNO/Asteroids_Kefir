using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

namespace _Source.CodeBase.Meta.Services.ScoreManager
{
    public class ScoreRepositoryController : IScoreRepositoryController
    {
        private const string SCORES_KEY = "AllScores";
        
        public List<int> Load()
        {
            if (!PlayerPrefs.HasKey(SCORES_KEY))
                return new List<int>();

            string json = PlayerPrefs.GetString(SCORES_KEY);
            return JsonConvert.DeserializeObject<List<int>>(json) ?? new List<int>();
        }
        
        public void Save(int score)
        {
            List<int> scores = Load();
            scores.Add(score);
            
            string json = JsonConvert.SerializeObject(scores);
            PlayerPrefs.SetString(SCORES_KEY, json);
            PlayerPrefs.Save();
        }
        
#if UNITY_EDITOR
        [MenuItem("Tools/Clear Scores PlayerPrefs")]
#endif
        private static void ClearAllScores()
        {
            PlayerPrefs.DeleteKey(SCORES_KEY);
            PlayerPrefs.Save();
        }
    }
}