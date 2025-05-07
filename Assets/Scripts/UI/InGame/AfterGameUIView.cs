using SaveData;
using TMPro;
using UnityEngine;

namespace UI.InGame
{
    public class AfterGameUIView : MonoBehaviour
    {
        [SerializeField] private TMP_Text indexText, nameText, descriptionText, latestScoreText, highestScoreText;
        
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
    }
}