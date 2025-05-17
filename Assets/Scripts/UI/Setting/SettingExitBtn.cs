using Setting;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Setting
{
    public class SettingExitBtn: MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            SettingManager.Instance.Off();
        }
    }
}