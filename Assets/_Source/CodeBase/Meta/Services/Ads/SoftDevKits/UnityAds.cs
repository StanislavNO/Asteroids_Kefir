namespace Assets._Source.CodeBase.Meta.Services.Ads.SoftDevKits
{
    internal abstract class UnityAds
    {
        protected readonly string InterstitialId;
        protected readonly string RewardedId;

        public UnityAds()
        {
#if UNITY_IOS
            InterstitialId = UnityAdUnitId.Interstitial_iOS.ToString();
            RewardedId = UnityAdUnitId.Rewarded_iOS.ToString();
#elif UNITY_ANDROID
            InterstitialId = UnityAdUnitId.Interstitial_Android.ToString();
            RewardedId = UnityAdUnitId.Rewarded_Android.ToString();
#endif
        }
    }
}