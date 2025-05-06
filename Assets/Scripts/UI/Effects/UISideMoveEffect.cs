using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Effects
{
    public class UISideMoveEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private float amount, duration;
        [SerializeField] private RectTransform rect;

        public void OnPointerEnter(PointerEventData eventData)
        {
            rect.DOAnchorPosX(amount, duration).SetEase(Ease.OutQuint);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            rect.DOAnchorPosX(0f, duration).SetEase(Ease.OutQuint);
        }
    }
}
