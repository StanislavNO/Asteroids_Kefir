using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Source.Code_base
{
    public class GameSceneManager
    {
        private readonly ICoroutineRunner _runner;

        private int _nextSceneIndex;

        public GameSceneManager(ICoroutineRunner runner)
        {
            _nextSceneIndex = (int)SceneNames.Game;
            _runner = runner;
        }

        public void ReloadCurrentScene() =>
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        public void LoadNextSceneAsync(Action loadComplete = null) =>
            _runner.StartCoroutine(LoadSceneAsync(loadComplete));

        public void SetNextScene(SceneNames name) =>
            _nextSceneIndex = (int)name;

        private IEnumerator LoadSceneAsync(Action loadComplete)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(_nextSceneIndex);

            while (asyncLoad.isDone == false)
                yield return null;

            loadComplete?.Invoke();
        }
    }
}
