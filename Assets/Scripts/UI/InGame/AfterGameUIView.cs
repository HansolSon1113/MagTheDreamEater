using TMPro;
using UnityEngine;

namespace UI.InGame
{
    public class AfterGameUIView : MonoBehaviour
    {
        [SerializeField] private TMP_Text indexText, nameText, latestScoreText, highestScoreText, latestHealthText, highestHealthText;
        
        public int index
        {
            set => indexText.text = "Stage " + (value + 1);
        }

        public string name
        {
            set => nameText.text = value;
        }

        public int latestScore
        {
            set => latestScoreText.text = value.ToString();
        }

        public int highestScore
        {
            set => highestScoreText.text = value.ToString();
        }

        public int latestHealth
        {
            set => latestHealthText.text = value.ToString();
        }

        public int highestHealth
        {
            set => highestHealthText.text = value.ToString();
        }
    }
}