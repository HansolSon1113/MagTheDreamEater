using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Setting
{
    public class SettingExitOverlay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private GameObject overlay;

        public void OnPointerEnter(PointerEventData eventData)
        {
            overlay.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            overlay.SetActive(false);
        }
    }
}