using SaveData;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Setting
{
    public class SettingMouseManager: MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private SettingData settingData;
        [SerializeField] private GameObject selectOverlay;

        public void OnPointerEnter(PointerEventData eventData)
        {
            selectOverlay.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            selectOverlay.SetActive(false);
        }
    }
}