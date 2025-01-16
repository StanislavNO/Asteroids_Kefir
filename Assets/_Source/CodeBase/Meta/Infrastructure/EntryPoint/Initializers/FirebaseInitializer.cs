using Assets._Source.CodeBase.Meta.Services.RemoteConfig;
using Cysharp.Threading.Tasks;
using Firebase;
using Firebase.Extensions;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._Source.CodeBase.Meta.Infrastructure.EntryPoint.Initializers
{
    internal class FirebaseInitializer : ISDKInitializer
    {
        private readonly RemoteConfigInitializer _configInitializer;

        public FirebaseInitializer(RemoteConfigInitializer remoteConfigInitializer)
        {
            _configInitializer = remoteConfigInitializer;
        }

        public async UniTask Init()
        {
            await FirebaseApp
                .CheckAndFixDependenciesAsync()
                .ContinueWithOnMainThread(OnDependencyStatusReceived);
        }

        private async void OnDependencyStatusReceived(Task<DependencyStatus> task)
        {
            try
            {
                if (task.IsCompletedSuccessfully == false)
                    throw new Exception("Could not resolve all Firebase dependencies", task.Exception);

                var status = task.Result;

                if (status != DependencyStatus.Available)
                    throw new Exception($"Could not resolve all Firebase dependencies {status}");

                Debug.Log("Firebase initialized successfully!");
                Debug.Log(status);

                await _configInitializer.FetchDataAsync();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }
}