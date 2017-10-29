using Assets.Scripts.Views;
using UnityEngine;
using UnityEngine.UI;

namespace LabyrinthEscape.Loader
{
    public class LoaderView : MonoBehaviour
    {
        #region MonoSingleton

        private LoaderView()
        {
        }

        private static LoaderView _instance;

        private static LoaderView Instance
        {
            get { return _instance; }
        }

        public void Awake()
        {
            if (_instance != null)
            {
                Destroy(_loaderCanvas.gameObject);
                return;
            }

            _instance = this;
            Hide();
        }

        #endregion

        [SerializeField] private Text _currentProgressText;

        [SerializeField] private Slider _loaderSlider;

        [SerializeField] private Canvas _loaderCanvas;

        public static void Hide()
        {
            Instance.gameObject.SetActive(false);
        }

        public static void Show()
        {
            SoundManagerView.Instance.StopMusic();
            Instance._loaderSlider.gameObject.SetActive(true);
            Instance.gameObject.SetActive(true);
        }

        public static void SetProgress(float progressValue)
        {
            Instance._loaderSlider.value = progressValue;
            Instance._currentProgressText.text = string.Format("{0}%", Mathf.RoundToInt(progressValue * 100));
        }
    }
}