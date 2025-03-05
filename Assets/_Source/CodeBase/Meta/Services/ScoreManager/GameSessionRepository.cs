using System.Collections.Generic;
using Assets._Source.CodeBase.Core.Infrastructure.Services.Score;
using Newtonsoft.Json;
using UnityEngine;

namespace _Source.CodeBase.Meta.Services.ScoreManager
{
    public class GameSessionRepository : IScoreRepository
    {
        private const string DATAS_KEY = "AllSessions";
        
        public List<GameSessionData> Load()
        {
            if (PlayerPrefs.HasKey(DATAS_KEY) == false)
                return new List<GameSessionData>();

            string json = PlayerPrefs.GetString(DATAS_KEY);
            return JsonConvert.DeserializeObject<List<GameSessionData>>(json) ?? new List<GameSessionData>();
        }
        
        public void Save(GameSessionData data)
        {
            List<GameSessionData> datas = Load();
            datas.Add(data);
            
            string json = JsonConvert.SerializeObject(datas);
            PlayerPrefs.SetString(DATAS_KEY, json);
            PlayerPrefs.Save();
        }
        
#if UNITY_EDITOR
        [UnityEditor.MenuItem("Tools/Clear Scores PlayerPrefs")]
#endif
        private static void ClearAllScores()
        {
            PlayerPrefs.DeleteKey(DATAS_KEY);
            PlayerPrefs.Save();
        }
    }
}