using UnityEngine.Advertisements;

namespace Assets._Source.CodeBase.Meta.Services.Ads.SoftDevKits
{
    internal class UnityAdsLoader : UnityAds, IAdsLoader, IUnityAdsLoadListener
    {
        public void LoadInterstitial()
        {
            Advertisement.Load(InterstitialId, this);
        }

        public void LoadRewarded()
        {
            Advertisement.Load(RewardedId, this);
        }

        public void OnUnityAdsAdLoaded(string placementId)
        {
        }

        public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
        {
        }
    }
}