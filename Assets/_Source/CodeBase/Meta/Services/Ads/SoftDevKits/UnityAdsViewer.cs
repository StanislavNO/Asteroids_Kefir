using Cysharp.Threading.Tasks;
using UnityEngine.Advertisements;

namespace Assets._Source.CodeBase.Meta.Services.Ads.SoftDevKits
{
    internal class UnityAdsViewer : UnityAds, IAdsViewer, IUnityAdsShowListener
    {
        private UniTaskCompletionSource<bool> _isShowComplete;

        public async UniTask ShowInterstitial()
        {
            Advertisement.Show(InterstitialId, this);

            _isShowComplete = new UniTaskCompletionSource<bool>();
            await _isShowComplete.Task;
        }

        public async UniTask<bool> ShowReward()
        {
            Advertisement.Show(RewardedId, this);

            _isShowComplete = new UniTaskCompletionSource<bool>();
            return await _isShowComplete.Task;
        }

        public void OnUnityAdsShowClick(string placementId)
        {
        }

        public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
        {
            _isShowComplete?.TrySetResult(true);
        }

        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
        {
            _isShowComplete?.TrySetResult(false);
        }

        public void OnUnityAdsShowStart(string placementId)
        {
        }
    }
}