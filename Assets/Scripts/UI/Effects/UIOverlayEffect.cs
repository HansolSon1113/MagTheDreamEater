using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Effects
{
    public abstract class UIOverlayEffect : MonoBehaviour, IPointerEnterHandler
    {
        [SerializeField] private float duration;
        [SerializeField] private RectTransform rect;

        public void OnPointerEnter(PointerEventData eventData)
        {
            rect.DOMove(transform.position, duration);
            Do();
        }

        protected abstract void Do();
    }
}