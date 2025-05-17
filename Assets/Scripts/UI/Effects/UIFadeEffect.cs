using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Effects
{
    public class UIFadeEffect : FadeEffect
    {
        protected override void Do() {}
    }
    
    public abstract class FadeEffect : MonoBehaviour
    {
        [SerializeField] private float duration;
        private Image image;
        public static FadeEffect Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            image = GetComponent<Image>();

            FadeIn();
        }

        private void FadeIn()
        {
            image.DOFade(0, duration).SetEase(Ease.InQuad).OnComplete(() =>
            {
                gameObject.SetActive(false);
                Do();
            });
        }

        protected abstract void Do();

        public void FadeOut(Action action)
        {
            gameObject.SetActive(true);
            image.DOFade(1, duration)
                .SetEase(Ease.InQuad)
                .OnComplete(() => action?.Invoke());
        }
    }
}