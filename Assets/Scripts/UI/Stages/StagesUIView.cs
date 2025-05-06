using DG.Tweening;
using TMPro;
using UnityEngine;

namespace UI.Stages
{
    public class StagesUIView : MonoBehaviour
    {
        [SerializeField] private RectTransform panel;
        [SerializeField] private TMP_Text indexText, nameText, descriptionText, latestScoreText, highestScoreText, latestTimeText, fastestTimeText;

        public int index
        {
            set => indexText.text = "Stage " + (value + 1);
        }

        public string name
        {
            set => nameText.text = value;
        }

        public string description
        {
            set => descriptionText.text = value;
        }

        public int latestScore
        {
            set => latestScoreText.text = value.ToString();
        }

        public int highestScore
        {
            set => highestScoreText.text = value.ToString();
        }

        public float latestTime
        {
            set => latestTimeText.text = value.ToString();
        }

        public float fastestTime
        {
            set => fastestTimeText.text = value.ToString();
        }

        public void Show()
        {
            panel.DOAnchorPosX(0, 1f);
        }

        public void UnShow()
        {
            panel.DOAnchorPosX(1000, 1f);
        }
    }
}