using Setting;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Setting
{
    public class SettingUIView : MonoBehaviour
    {
        [SerializeField] private Slider totalVolumeSlider;
        [SerializeField] private Slider popVolumeSlider;
        [SerializeField] private Slider musicVolumeSlider;
        private SettingManager settingManager;

        public float totalVolume
        {
            set => settingManager.totalVolume = totalVolumeSlider.value = value;
        }

        public float popVolume
        {
            set => settingManager.popVolume = popVolumeSlider.value = value;
        }

        public float musicVolume
        {
            set => settingManager.musicVolume = musicVolumeSlider.value = value;
        }

        private void Start()
        {
            settingManager = SettingManager.Instance;

            totalVolume = settingManager.settingData.settingDataElements.totalVolume;
            popVolume = settingManager.settingData.settingDataElements.popVolume;
            musicVolume = settingManager.settingData.settingDataElements.musicVolume;

            totalVolumeSlider.onValueChanged.AddListener((value) => { totalVolume = value; });
            popVolumeSlider.onValueChanged.AddListener((value) => { popVolume = value; });
            musicVolumeSlider.onValueChanged.AddListener((value) => { musicVolume = value; });
        }
    }
}