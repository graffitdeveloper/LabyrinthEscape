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
            get
            {
                return _instance;
            }
        }

        #endregion

        [SerializeField] private Text _currentProgressText;

        [SerializeField] private Slider _loaderSlider;

        public void Awake()
        {
            if(_instance != null)
                Debug.LogError("More than one instances of loader, be prepared for unexpected issues");

            _instance = this;
            Hide();
        }

        public static void Hide()
        {
            Instance.gameObject.SetActive(false);
        }

        public static void Show()
        {
            Instance.gameObject.SetActive(true);
        }

        public static void SetProgress(float progressValue)
        {
            Instance._loaderSlider.value = progressValue;
            Instance._currentProgressText.text = Mathf.RoundToInt(progressValue * 100).ToString();
        }
    }
}