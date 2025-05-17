using System.Collections;
using Interfaces;
using SaveData;
using UI.Effects;
using UI.InGame;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace InGame.Managers
{
    public class AfterGameManager : MonoBehaviour, IEscapable
    {
        private ISaveData gameData = GameDataContainer.gameData;
        private InputActionMap afterGameMap;
        public InputAction escapeAction { get; set; }
        [SerializeField] private AfterGameUIView afterGameUIView;
        private bool sceneEnd = false;
        [SerializeField] private float duration = 10f;
        [SerializeField] private GameObject gameOverPanel;

        public void Awake()
        {
            afterGameMap = InputSystem.actions.FindActionMap("AfterGame");
            afterGameMap.Enable();

            escapeAction = InputSystem.actions.FindAction("Continue");
            escapeAction.performed += OnEscapePerformed;
            escapeAction.Enable();
        }

        private void Start()
        {
            var data = gameData.gameDataElements;
            if (data.latestHealth[data.currentStage] <= 0)
            {
                gameOverPanel.SetActive(true);
                sceneEnd = true;
            }
            else
            {
                var info = GameDataContainer.currentStage;

                afterGameUIView.index = data.currentStage;
                afterGameUIView.name = info.stageName;
                afterGameUIView.description = info.stageDescription;
                afterGameUIView.latestScore = data.latestScores[data.currentStage];
                afterGameUIView.highestScore = data.highestScores[data.currentStage];
                afterGameUIView.latestHealth = data.latestHealth[data.currentStage];
                afterGameUIView.highestHealth = data.highestHealth[data.currentStage];

                StartCoroutine(EndScene());
            }
        }

        private IEnumerator EndScene()
        {
            yield return new WaitForSeconds(duration);

            sceneEnd = true;
        }
    
        public void OnDestroy()
        {
            escapeAction.performed -= OnEscapePerformed;
            escapeAction.Disable();
            afterGameMap.Disable();
        }

        public void OnEscapePerformed(InputAction.CallbackContext ctx)
        {
            Escape();
        }

        public void Escape()
        {
            if(sceneEnd) UIFadeEffect.Instance.FadeOut(() => { SceneManager.LoadScene("Stages"); });
        }
    }
}
