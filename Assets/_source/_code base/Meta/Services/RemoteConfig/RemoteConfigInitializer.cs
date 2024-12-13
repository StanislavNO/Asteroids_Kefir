using Firebase.Extensions;
using Firebase.RemoteConfig;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._source._code_base.Meta.Services.RemoteConfig
{
    internal class RemoteConfigInitializer
    {
        public static bool IsComplete { get; private set; } = false;

        public Task FetchDataAsync()
        {
            Task fetchTask = FirebaseRemoteConfig
                            .DefaultInstance
                            .FetchAsync(TimeSpan.Zero);

            return fetchTask.ContinueWithOnMainThread(FetchComplete);
        }

        private void FetchComplete(Task fetchTask)
        {
            if (!fetchTask.IsCompleted)
            {
                Debug.LogError("Retrieval hasn't finished");
                return;
            }

            FirebaseRemoteConfig remoteConfig = FirebaseRemoteConfig.DefaultInstance;
            ConfigInfo info = remoteConfig.Info;

            if (info.LastFetchStatus != LastFetchStatus.Success)
            {
                Debug.LogError($"{nameof(FetchComplete)} was unsuccessful\n{nameof(info.LastFetchStatus)}: {info.LastFetchStatus}");
                return;
            }

            remoteConfig.ActivateAsync()
                .ContinueWithOnMainThread(task =>
                Debug.Log($"Remote data loaded and ready for use. Last fetch time {info.FetchTime}"));

            IsComplete = true;
        }
    }
}