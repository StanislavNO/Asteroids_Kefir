using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Advertisements;

namespace Assets._source._code_base.Meta.Services.Ads.SoftDevKits
{
    internal class UnityAdsViewer : UnityAds, IAdsViewer, IUnityAdsShowListener
    {
        private TaskCompletionSource<bool> _initCompletion;

        public void ShowInterstitial()
        {
            Advertisement.Show(InterstitialId);
        }

        public async Task ShowReward()
        {
            Advertisement.Show(RewardedId);

            _initCompletion = new TaskCompletionSource<bool>();
            await _initCompletion.Task;
        }

        public void OnUnityAdsShowClick(string placementId)
        {
        }

        public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
        {
            if (placementId == RewardedId && showCompletionState.Equals(UnityAdsCompletionState.COMPLETED))
            {
                Debug.Log("OnUnityAdsShowComplete");
                _initCompletion?.SetResult(true);
            }
            else
            {
                _initCompletion?.SetResult(false);
            }
        }

        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
        {
            _initCompletion?.SetResult(false);
        }

        public void OnUnityAdsShowStart(string placementId)
        {
        }
    }
}
