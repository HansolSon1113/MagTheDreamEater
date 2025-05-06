using SaveData;
using TMPro;
using UnityEngine;

namespace UI.InGame
{
    public class AfterGameUIView : MonoBehaviour
    {
        [SerializeField] private TMP_Text indexText, nameText, descriptionText, latestScoreText, highestScoreText, latestTimeText, fastestTimeText;
        private ISaveData gameData = GameDataContainer.gameData;
        
        private void Start()
        {
            var data = gameData.gameDataElements;
            var info = GameDataContainer.currentStage;
            indexText.text = "Stage " + data.currentStage;
            nameText.text = info.stageName;
            descriptionText.text = info.stageDescription;
            latestScoreText.text = data.latestScores[data.currentStage].ToString();
            highestScoreText.text = data.highestScores[data.currentStage].ToString();
            latestTimeText.text = data.latestTimes[data.currentStage].ToString();
            fastestTimeText.text = data.fastestTimes[data.currentStage].ToString();
        }
    }
}