using UnityEditor;
using UnityEngine;
using Zenject;

namespace Assets._source._code_base.Meta.Services.InApp
{
    public class PurchaseSaver : IReadonlyStore, IInitializable
    {
        private const string NO_ADS = "noads";

        public AdsStatus AdsStatus { get; private set; } = AdsStatus.Active;

        public void Initialize()
        {
            Load();
        }

        public void Save()
        {
            PlayerPrefs.SetInt(NO_ADS, (int)AdsStatus.Deactivate);
            PlayerPrefs.Save();
            AdsStatus = AdsStatus.Deactivate;
        }

        private void Load()
        {
            if (PlayerPrefs.GetInt(NO_ADS) == (int)AdsStatus.Active)
                AdsStatus = AdsStatus.Active;
            else
                AdsStatus = AdsStatus.Deactivate;
        }

#if UNITY_EDITOR
        [MenuItem("Tools/Clear PlayerPrefs Purchase")]
        private static void Clear()
        {
            PlayerPrefs.DeleteKey(NO_ADS);
        }
#endif
    }
}