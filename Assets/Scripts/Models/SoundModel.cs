using UnityEngine;
using Assets.Scripts.Enum;

namespace Assets.Scripts.Models
{
    public class SoundModel : MonoBehaviour
    {
        public SoundEnum SoundType { get { return _soundEnum; } }

        [SerializeField] private SoundEnum _soundEnum;

        private AudioSource _audioSource;

        #region Unity

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        #endregion

        #region Public

        public void PlayMusic()
        {
            _audioSource.Play();
        }

        public void StopMusic()
        {
            _audioSource.Stop();
        }

        #endregion
    }
}