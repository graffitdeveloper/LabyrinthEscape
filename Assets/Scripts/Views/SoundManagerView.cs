using System.Collections.Generic;
using LabyrinthEscape.GameSettingsControls;
using UnityEngine;

namespace Assets.Scripts.Views
{
    public class SoundManagerView : MonoBehaviour
    {
        #region MonoSingleton

        private SoundManagerView()
        {
        }

        private static SoundManagerView _instance;

        public static SoundManagerView Instance
        {
            get { return _instance; }
        }

        public void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
        }

        #endregion

        [SerializeField] private AudioSource _menuChooseEffect;

        [SerializeField] private AudioSource _musicAudioSource;

        [SerializeField] private AudioSource _congratsEffect;

        [SerializeField] private List<AudioClip> _musicList;

        [SerializeField] private AudioClip _cricketEffect;

        [SerializeField] private AudioClip _titleTrack;

        public bool IsMusicPlaying { get; private set; }

        public void StopMusic()
        {
            IsMusicPlaying = false;
            _musicAudioSource.Stop();
        }

        public void PlayCricket()
        {
            if (!GameSettings.Instance.IsSoundEnable)
            {
                _musicAudioSource.Stop();
                return;
            }

            _musicAudioSource.clip = _cricketEffect;
            _musicAudioSource.Play();
        }

        public void PlayRandomActionMusic()
        {
            if (!GameSettings.Instance.IsMusicEnable)
            {
                StopMusic();
                return;
            }

            _musicAudioSource.clip = _musicList[Random.Range(0, _musicList.Count)];
            _musicAudioSource.Play();

            IsMusicPlaying = true;
        }

        public void PlayMenuChooseEffect()
        {
            if (GameSettings.Instance.IsSoundEnable)
                _menuChooseEffect.Play();
        }

        public void PlayCongratsEffect()
        {
            if (GameSettings.Instance.IsSoundEnable)
                _congratsEffect.Play();
        }

        public void PlayTitle()
        {
            if (!GameSettings.Instance.IsMusicEnable)
                return;

            IsMusicPlaying = true;

            _musicAudioSource.clip = _titleTrack;
            _musicAudioSource.Play();
        }
    }
}