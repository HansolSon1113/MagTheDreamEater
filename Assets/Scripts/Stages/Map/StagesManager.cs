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
            }
        }
        private InputActionMap stagesActionMap;
        public InputAction moveAction { get; set; }
        public InputAction submitAction { get; set; }
        public InputAction escapeAction { get; set; }
        [SerializeField] private float duration;
        [SerializeField] private StagesUIView stagesUIView;
        [SerializeField] private Transform camera;
        public StagesMapSO stageMap { get; set; }
        public StagesMapSO data
        {
            set => stageMap = value;
        }
        public List<Transform> stageTransforms { get; set; }
        private ISaveData gameData = GameDataContainer.gameData;

        public bool detailPanelOn
        {
            get => stagesUIView.detailPanelOn;
            set => stagesUIView.detailPanelOn = value;
        }
        private bool moving = false;
        public static StagesManager Instance;

        public void Awake()
        {
            Instance = this;
            
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
        }

        private void Start()
        {
            currentStage = gameData.gameDataElements.currentStage;
            transform.position = stageTransforms[currentStage].position;
            camera.position = new Vector3(transform.position.x, transform.position.y, -10);
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
            
            ISaveable saveData = GameDataContainer.gameData;
            saveData.Save();
        }

        public void OnMovePerformed(InputAction.CallbackContext ctx)
        {
            var value = ctx.ReadValue<Vector2>();
            Move(value);
        }

        public void Move(Vector2 value)
        {
            if (detailPanelOn)
            {
                if (!Mathf.Approximately(value.x, 0))
                {
                    stagesUIView.stageMenu = value.x < 0 ? StageMenu.Start : StageMenu.Back;
                }
            }
            else if (stagesUIView.escapePanelOn)
            {
                var delta = Mathf.Abs(value.x) > Mathf.Abs(value.y) ? value.x : value.y;
                if (Mathf.Approximately(delta, 0)) return;
                var curr = (int)stagesUIView.stageEscape;
                var next = (curr + (delta > 0 ? -1 : 1) + stagesUIView.cnt) % stagesUIView.cnt;
                stagesUIView.stageEscape = (StageEscape)next;
            }
            else
            {
                if (moving) return;
                
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

                moving = true;
                camera.DOMove(
                    new Vector3(stageTransforms[currentStage].position.x, stageTransforms[currentStage].position.y,
                        -10), 1f).OnComplete(
                    () => { moving = false; });
            }
        }

        public void OnSubmitPerformed(InputAction.CallbackContext ctx)
        {
            if (moving) return;
            
            if (stagesUIView.escapePanelOn)
            {
                StagesFinish.Instance.Submit();
            }
            else
            {
                Submit();
            }
        }

        public void Submit()
        {
            if (detailPanelOn)
            {
                if (stagesUIView.stageMenu == StageMenu.Start)
                {
                    GameDataContainer.currentStage = stageMap.nodes[currentStage];
                    SceneManager.LoadScene("InGame");
                }
                else
                {
                    Escape();
                }
            }
            else
            {
                stagesUIView.Show();
                detailPanelOn = true;
            }
        }

        public void OnEscapePerformed(InputAction.CallbackContext ctx)
        {
            if (detailPanelOn)
            {
                Escape();
            }
            else if(stagesUIView.escapePanelOn)
            {
                stagesUIView.escapePanelOn = false;
            }
            else
            {
                stagesUIView.escapePanelOn = true;
            }
        }

        public void Escape()
        {
            stagesUIView.UnShow();
            detailPanelOn = false;
        }
    }
}