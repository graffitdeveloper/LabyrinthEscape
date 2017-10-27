using System;
using System.Collections;
using LabyrinthEscape.GameManagerControls;
using LabyrinthEscape.Loader;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace LabyrinthEscape.HudView
{
    public class HudView : MonoBehaviour
    {
        [SerializeField] private Text _timerText;
        [SerializeField] public GameObject _completedLayout;
        [SerializeField] public GameObject _simpleHud;
        [SerializeField] private Text _largeTimeText;

        private float _currentTime;

        /// <summary>
        /// Возвращает строку в формате 00д 00ч 00м 00с из секунд.
        /// </summary>
        /// <param name="seconds"></param>
        /// <returns></returns>
        public static string GetFormattedTimeFromSeconds(int seconds)
        {
            var timeSpan = TimeSpan.FromSeconds(seconds);
            return string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
        }

        public void Update()
        {
            ManageTimer();

            if (GameManager.Instance.IsGameFinished && !_completedLayout.activeInHierarchy)
            {
                _largeTimeText.text = _timerText.text;
                _completedLayout.SetActive(true);
                _simpleHud.SetActive(false);
            }

            if (!GameManager.Instance.IsGameStarted && _completedLayout.activeInHierarchy)
            {
                _completedLayout.SetActive(false);
                _simpleHud.SetActive(true);
            }
        }

        private void ManageTimer()
        {
            if (!GameManager.Instance.IsGameStarted)
                _timerText.text = "00:00:00";

            if (GameManager.Instance.IsGameStarted &&
                !GameManager.Instance.IsGamePaused && !GameManager.Instance.IsGameFinished)
            {
                _currentTime += Time.deltaTime;
                _timerText.text = GetFormattedTimeFromSeconds(Mathf.RoundToInt(_currentTime));
            }
        }

        public void OnRetryPressed()
        {
            LoaderView.Show();
            LoaderView.SetProgress(0f);

            StartCoroutine(ReloadSceneAsync());
        }

        IEnumerator ReloadSceneAsync()
        {
            var asyncLoad = SceneManager.LoadSceneAsync("Game");

            while (!asyncLoad.isDone)
                yield return null;

            LoaderView.SetProgress(0.1f);
        }

        public void OnMainMenuButtonPressed()
        {
            LoaderView.Show();
            LoaderView.SetProgress(0f);

            StartCoroutine(LoadMainMenuAsync());
        }

        IEnumerator LoadMainMenuAsync()
        {
            var asyncLoad = SceneManager.LoadSceneAsync("MainMenu");

            while (!asyncLoad.isDone)
                yield return null;

            LoaderView.SetProgress(0.1f);
        }
    }
}