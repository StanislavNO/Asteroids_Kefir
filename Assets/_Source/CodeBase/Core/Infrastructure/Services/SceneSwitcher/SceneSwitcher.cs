using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets._Source.CodeBase.Core.Infrastructure.Services.SceneSwitcher
{
    public class SceneSwitcher : IGameStartSignal
    {
        public event Action Starting;

        public async void LoadGameAsync(Action loadComplete = null, SceneNames scene = SceneNames.Menu)
        {
            await LoadSceneAsync(scene);
            loadComplete?.Invoke();
        }

        private async UniTask LoadSceneAsync(SceneNames scene)
        {
            AsyncOperation asyncLoad = SceneManager
                .LoadSceneAsync((int)scene);

            while (!asyncLoad.isDone)
                await UniTask.Yield();

            Starting?.Invoke();
        }
    }
}