using Cysharp.Threading.Tasks;
using System;
using Assets._Source.CodeBase.Meta.Services.Analytics;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets._Source.CodeBase.Core.Infrastructure.Services.SceneSwitcher
{
    public class SceneSwitcher
    {
        private readonly IEventWriter _analytics;

        public SceneSwitcher(IEventWriter eventWriter)
        {
            _analytics = eventWriter;
        }
        
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

            _analytics.WriteGameStart();
        }
    }
}