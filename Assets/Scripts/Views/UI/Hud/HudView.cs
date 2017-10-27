using System;
using LabyrinthEscape.GameManagerControls;
using UnityEngine;
using UnityEngine.UI;

namespace LabyrinthEscape.HudView
{
    public class HudView : MonoBehaviour
    {
        [SerializeField] private Text _timerText;
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
    }
}