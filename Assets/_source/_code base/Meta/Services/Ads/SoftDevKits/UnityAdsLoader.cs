using Assets._source._code_base.Meta.Services.Ads.SoftDevKits;
using UnityEngine.Advertisements;

namespace Assets._source._code_base.Meta.Services.Ads.SDKs
{
    internal class UnityAdsLoader : UnityAds, IAdsLoader , IUnityAdsLoadListener
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