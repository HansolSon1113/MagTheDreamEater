using System.Collections;
using SaveData;
using UI.Effects;
using UI.InGame;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

namespace InGame.Managers
{
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager Instance;
        public bool scoreCount = true;
        private bool clear = false;
        private int _score;
        private int _health;
        private float _stageTime;
        private InGameUIView inGameUIView;

        public int score
        {
            get => _score;
            set
            {
                if (_score == value || value < 0) return;
            
                _score = value;
                inGameUIView.score = value;
            }
        }
        
        public int health
        {
            get => _health;
            set
            {
                if (_health == value || health < 0) return;
            
                _health = value;
                inGameUIView.health = value;
            }
        }

        public float stageTime
        {
            get => _stageTime;
            set
            {
                if (Mathf.Approximately(_stageTime, value)) return;
            
                _stageTime = value;
            }
        }

        public void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            inGameUIView = InGameUIView.Instance;
            health = 100;

            inGameUIView.stageNum = int.Parse(GameDataContainer.currentStage.stageIndex);
        }

        private void Update()
        {
            if (scoreCount)
            {
                stageTime += Time.deltaTime;
            }
        }

        public void Stop()
        {
            scoreCount = false;

            StartCoroutine(WrapUp());
        }

        private IEnumerator WrapUp()
        {
            if (_health > 0) clear = true;
            
            yield return new WaitForSeconds(5f);

            FadeEffect.Instance.FadeOut(() =>
            {
                SceneManager.LoadScene("AfterGame");
            });
        }

        private void OnDestroy()
        {
            if (!clear) return;
            ISaveable gameData = GameDataContainer.gameData;

            var data = gameData.gameDataElements;
            data.clearedStages[data.currentStage] = true;
            data.latestScores[data.currentStage] = _score;
            if (data.highestScores[data.currentStage] < _score)
            {
                data.highestScores[data.currentStage] = _score;
            }
            data.latestTimes[data.currentStage] = _stageTime;
            if (data.fastestTimes[data.currentStage] > _stageTime || data.fastestTimes[data.currentStage] == 0)
            {
                data.fastestTimes[data.currentStage] = _stageTime;
            }
            data.latestHealth[data.currentStage] = _health;
            if (data.highestHealth[data.currentStage] < _health)
            {
                data.highestHealth[data.currentStage] = _health;
            }
                
            gameData.Save();
        }
    }
}