using System.Collections;
using LabyrinthEscape.Loader;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LabyrinthEscape.LabyrinthGeneratorControls
{
    public class SceneChanger : MonoBehaviour
    {
        #region Singleton

        private SceneChanger()
        {
        }

        private static SceneChanger _instance;

        public static SceneChanger Instance
        {
            get
            {
                if (_instance == null)
                {
                    var gameObject = new GameObject("SceneChanger");
                    _instance = gameObject.AddComponent<SceneChanger>();
                }

                return _instance;
            }
        }

        #endregion

        public void LoadGameScene()
        {
            LoaderView.Show();
            LoaderView.SetProgress(0f);

            StartCoroutine(LoadGameSceneAsync());
        }

        IEnumerator LoadGameSceneAsync()
        {
            var asyncLoad = SceneManager.LoadSceneAsync("Game");

            while (!asyncLoad.isDone)
                yield return null;

            LoaderView.SetProgress(0.1f);
        }


        public void LoadMainScene()
        {
            LoaderView.Show();
            LoaderView.SetProgress(0f);

            StartCoroutine(LoadMainSceneAsync());
        }

        IEnumerator LoadMainSceneAsync()
        {
            var asyncLoad = SceneManager.LoadSceneAsync("MainMenu");

            while (!asyncLoad.isDone)
            {
                LoaderView.SetProgress(asyncLoad.progress);
                yield return null;
            }
        }
    }
}