using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Effects
{
    public abstract class UIOverlayEffect : MonoBehaviour, IPointerEnterHandler
    {
        public float duration;
        public RectTransform rect;

        public void OnPointerEnter(PointerEventData eventData)
        {
            Do();
        }

        public abstract void Do();
    }
}