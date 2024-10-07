using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Source.Code_base
{
    public class SceneSwitcher
    {
        public async void LoadGameAsync(Action loadComplete = null)
        {
            await LoadSceneAsync();
            loadComplete?.Invoke();
        }

        private async UniTask LoadSceneAsync()
        {
            AsyncOperation asyncLoad = SceneManager
                .LoadSceneAsync((int)SceneNames.Game);

            while (!asyncLoad.isDone)
                await UniTask.Yield();
        }
    }
}
