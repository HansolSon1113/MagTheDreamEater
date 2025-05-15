using Setting;
using UnityEngine;

namespace Interfaces
{
    public interface IVolumeContainer
    {
        public float totalVolume { set; }
        public float popVolume { set; }
        public float musicVolume { set; }
    }

    public class AudioManager : MonoBehaviour, IVolumeContainer
    {
        [SerializeField] private AudioSource popAudioSource;
        public AudioSource musicAudioSource;
        private SettingManager volumeContainer;
        public static AudioManager Instance;

        public float totalVolume
        {
            set
            {
                popAudioSource.volume = volumeContainer.popVolume * value;
                musicAudioSource.volume = volumeContainer.musicVolume * value;
            }
        }

        public float popVolume
        {
            set => popAudioSource.volume = value * volumeContainer.totalVolume;
        }

        public float musicVolume
        {
            set => musicAudioSource.volume = value * volumeContainer.totalVolume;
        }

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            volumeContainer = SettingManager.Instance;
            volumeContainer.audioManager = this;
            
            totalVolume = volumeContainer.totalVolume;
            popVolume = volumeContainer.popVolume;
            musicVolume = volumeContainer.musicVolume;
        }

        public void PlayPop()
        {
            popAudioSource.Play();
        }
    }
}