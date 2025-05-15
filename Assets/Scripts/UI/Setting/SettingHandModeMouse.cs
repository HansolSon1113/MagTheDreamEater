using SaveData;
using Setting;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Setting
{
    public class SettingHandModeMouse: MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private HandMode handMode;
        [SerializeField] private SettingUIView settingUIView;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            settingUIView.handMode = handMode;
        }
    }
}