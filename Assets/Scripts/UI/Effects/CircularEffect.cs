using System;
using DG.Tweening;
using UnityEngine;

namespace UI.Effects
{
    public class CircularEffect : MonoBehaviour
    {
        [SerializeField] private float duration;
        public static CircularEffect Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            CircularIn();
        }

        public void CircularIn()
        {
            transform.DOScale(new Vector3(1, 1, 1), duration).SetEase(Ease.InQuad).OnComplete(() =>
            {
                gameObject.SetActive(false);
            });
        }

        public void CircularOut(Action action)
        {
            gameObject.SetActive(true);
            transform.DOScale(new Vector3(0.001f, 0.001f, 1), duration).SetEase(Ease.InQuad).OnComplete(() => action?.Invoke());
        }
    }
}
