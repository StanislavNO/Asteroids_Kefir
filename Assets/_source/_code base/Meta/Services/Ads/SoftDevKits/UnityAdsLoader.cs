using Assets._source._code_base.Meta.Services.Ads.SoftDevKits;
using UnityEngine.Advertisements;

namespace Assets._source._code_base.Meta.Services.Ads.SDKs
{
    internal class UnityAdsLoader : UnityAds, IAdsLoader
    {
        public void LoadInterstitial()
        {
            Advertisement.Load(InterstitialId);
        }

        public void LoadRewarded()
        {
            Advertisement.Load(RewardedId);
        }
    }
}
