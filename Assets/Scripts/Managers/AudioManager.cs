using Assets.Scripts.Controllers;
using Assets.Scripts.Enum;
using Assets.Scripts.Models;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Assets.Scripts.Managers
{
    public class AudioManager : MonoBehaviour
    {
        [Header("Mixers")]
        [SerializeField] private AudioMixer _mixer;

        [Header("Components")]
        [SerializeField] private List<SoundModel> _soundModels;

        private readonly string SOUND_MIXER_ID = "SfxVolume";
        private readonly string MUSIC_MIXER_ID = "MusicVolume";

        #region Unity

        private void Awake()
        {
            //_settingsManager.SettingsUpdatedAction += OnSettingsChanged;
        }

        private void OnSettingsChanged()
        {
            //bool isMusicOn = _settingsManager.IsMusicOn;
            //bool isSFXOn = _settingsManager.IsSoundOn;
            //_mixer.SetFloat(MUSIC_MIXER_ID, isMusicOn ? 0f : -80f);
            //_mixer.SetFloat(SOUND_MIXER_ID, isSFXOn ? 0f : -80f);
        }

        #endregion

        #region Public

        public void PlaySound(SoundEnum soundEnum)
        {
            for (int i = 0; i < _soundModels.Count; i++)
            {
                if (_soundModels[i].SoundType == soundEnum)
                {
                    _soundModels[i].PlayMusic();
                    break;
                }
            }
        }

        public void StopSound(SoundEnum soundEnum)
        {
            for (int i = 0; i < _soundModels.Count; i++)
            {
                if (_soundModels[i].SoundType == soundEnum)
                {
                    _soundModels[i].StopMusic();
                    break;
                }
            }
        }

        #endregion


        #region Private

        #endregion
    }
}