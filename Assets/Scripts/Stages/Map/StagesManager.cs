using System.Collections.Generic;
using DG.Tweening;
using Interfaces;
using SaveData;
using UI.Stages;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Stages.Map
{
    public class StagesManager : MonoBehaviour, IMovable, ISubmittable, IMapContainer, IEscapable
    {
        private int _currentStage;
        private int currentStage
        {
            get => _currentStage;
            set
            {
                _currentStage = value;

                var data = gameData.gameDataElements;
                var info = stageMap.nodes[currentStage];
                data.currentStage = currentStage;
                
                stagesUIView.index = currentStage;
                stagesUIView.name = info.stageName;
                stagesUIView.description = info.stageDescription;
                stagesUIView.latestScore = data.latestScores[currentStage];
                stagesUIView.highestScore = data.highestScores[currentStage];
                stagesUIView.latestTime = data.latestTimes[currentStage];
                stagesUIView.fastestTime = data.fastestTimes[currentStage];
            }
        }
        private InputActionMap stagesActionMap;
        public InputAction moveAction { get; set; }
        public InputAction submitAction { get; set; }
        public InputAction escapeAction { get; set; }
        [SerializeField] private float duration;
        [SerializeField] private StagesUIView stagesUIView;
        public StagesMapSO stageMap { get; set; }
        public StagesMapSO data
        {
            set => stageMap = value;
        }
        public List<Transform> stageTransforms { get; set; }
        private ISaveData gameData = GameDataContainer.gameData;
        private bool detailPanelOn = false;
        
        public void Start()
        {
            stagesActionMap = InputSystem.actions.FindActionMap("Stages");
            stagesActionMap.Enable();

            moveAction = InputSystem.actions.FindAction("Move");
            moveAction.performed += OnMovePerformed;
            moveAction.Enable();
            submitAction = InputSystem.actions.FindAction("Submit");
            submitAction.performed += OnSubmitPerformed;
            submitAction.Enable();
            escapeAction = InputSystem.actions.FindAction("Escape");
            escapeAction.performed += OnEscapePerformed;
            escapeAction.Enable();

            currentStage = gameData.gameDataElements.currentStage;
            transform.position = stageTransforms[currentStage].position;
        }

        public void OnDestroy()
        {
            moveAction.performed -= OnMovePerformed;
            submitAction.performed -= OnSubmitPerformed;
            escapeAction.performed -= OnEscapePerformed;
            moveAction.Disable();
            submitAction.Disable();
            escapeAction.Disable();

            stagesActionMap.Disable();
        }

        public void OnMovePerformed(InputAction.CallbackContext ctx)
        {
            stagesUIView.UnShow();
            detailPanelOn = false;
            
            var value = ctx.ReadValue<Vector2>();

            var node = stageMap.nodes[currentStage];

            var way = currentStage;
            if (!Mathf.Approximately(value.x, 0))
            {
                if (value.x > 0)
                {
                    way = int.Parse(node.right);
                }
                else
                {
                    way = int.Parse(node.left);
                }
            }

            if (!Mathf.Approximately(value.y, 0))
            {
                if (value.y > 0)
                {
                    way = int.Parse(node.up);
                }
                else
                {
                    way = int.Parse(node.down);
                }
            }

            if (currentStage == way) return;
            currentStage = way;

            transform.DOMove(stageTransforms[currentStage].position, duration);
        }

        public void OnSubmitPerformed(InputAction.CallbackContext ctx)
        {
            if (detailPanelOn)
            {
                GameDataContainer.currentStage = stageMap.nodes[currentStage];
                SceneManager.LoadScene("InGame");
            }
            else
            {
                stagesUIView.Show();
                detailPanelOn = true;
            }
        }
        
        public void OnEscapePerformed(InputAction.CallbackContext ctx)
        {
            if (!detailPanelOn) return;
            stagesUIView.UnShow();
            detailPanelOn = false;
        }
    }
}