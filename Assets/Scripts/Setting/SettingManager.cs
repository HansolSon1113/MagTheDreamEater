using Interfaces;
using SaveData;
using UnityEngine;

namespace Setting
{
    public class SettingManager : MonoBehaviour
    {
        public SettingData settingData;
        public static SettingManager Instance;
        public IVolumeContainer audioManager;
        [HideInInspector] public bool settingPanelOn;

        public float totalVolume
        {
            get => settingData.settingDataElements.totalVolume;
            set => settingData.settingDataElements.totalVolume = audioManager.totalVolume = value;
        }

        public float popVolume
        {
            get => settingData.settingDataElements.popVolume;
            set => settingData.settingDataElements.popVolume = audioManager.popVolume = value;
        }

        public float musicVolume
        {
            get => settingData.settingDataElements.musicVolume;
            set => settingData.settingDataElements.musicVolume = audioManager.musicVolume = value;
        }

        public HandMode handMode
        {
            get => settingData.settingDataElements.handMode;
            set => settingData.settingDataElements.handMode = value;
        }

        private void Awake()
        {
            if (Instance != this && Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            
            DontDestroyOnLoad(gameObject);
            Instance = this;
            settingData = GameDataContainer.settingData;
        }

        private void Start()
        {
            Off();
        }

        public void On()
        {
            settingPanelOn = true;
            Time.timeScale = 0f;
            gameObject.SetActive(true);
        }
        
        public void Off()
        {
            settingPanelOn = false;
            Time.timeScale = 1f;
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            if(Instance == this) settingData.Save();
        }
    }
}