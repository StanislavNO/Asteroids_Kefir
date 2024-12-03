using Firebase;
using Firebase.Extensions;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._source._code_base.Meta
{
    internal class FirebaseInitializer
    {
        public async Task Init(Action callback = null)
        {
            await FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(OnDependencyStatusReceived);
            callback?.Invoke();
        }

        private void OnDependencyStatusReceived(Task<DependencyStatus> task)
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
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }
}