using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Advertisements;

namespace Assets._source._code_base.Meta.Infrastructure.EntryPoint
{
    internal class UnityAdsInitializer : ISDKInitializer, IUnityAdsInitializationListener
    {
        private const string AndroidGameId = "5745468";
        private const string IosGameId = "5745469";

        private bool _testMode = true;
        private UniTaskCompletionSource<bool> _initCompletion;

        public static string GameId { get; private set; }

        public async UniTask Init()
        {
#if UNITY_IOS
            _gameId = IosGameId;
#elif UNITY_ANDROID
            GameId = AndroidGameId;
#elif UNITY_EDITOR
            _gameId = AndroidGameId; //Only for testing the functionality in the Editor
#endif
            if (!Advertisement.isInitialized && Advertisement.isSupported)
            {
                _initCompletion = new UniTaskCompletionSource<bool>();
                Advertisement.Initialize(GameId, _testMode, this);
            }

            await _initCompletion.Task;
        }

        public void OnInitializationComplete()
        {
            Debug.Log("Unity Ads initialization complete.");
            _initCompletion?.TrySetResult(true);
        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            Debug.Log($"Unity Ads Initialization Failed: {error} - {message}");
            _initCompletion?.TrySetResult(false);
        }
    }
}