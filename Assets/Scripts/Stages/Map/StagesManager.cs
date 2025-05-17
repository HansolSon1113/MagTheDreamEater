using System.Collections.Generic;
using DG.Tweening;
using Interfaces;
using SaveData;
using Setting;
using UI.Effects;
using UI.Lobby;
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
                var info = stageMap.nodes[value];
                data.currentStage = value;

                stagesUIView.index = _currentStage;
                stagesUIView.name = info.stageName;
                stagesUIView.description = info.stageDescription;
                stagesUIView.latestScore = data.latestScores[value];
                stagesUIView.highestScore = data.highestScores[value];
            }
        }
        private InputActionMap stagesActionMap;
        public InputAction moveAction { get; set; }
        public InputAction submitAction { get; set; }
        public InputAction escapeAction { get; set; }
        [SerializeField] private float duration;
        private StagesUIView stagesUIView;
        [SerializeField] private Transform camera;
        private SettingManager settingManager;
        public StagesMapSO stageMap { get; set; }

        public StagesMapSO data
        {
            set => stageMap = value;
        }

        public List<GameObject> stageObjects { get; set; }
        private List<GameObject> floatingEffects = new();
        private ISaveData gameData = GameDataContainer.gameData;

        public bool detailPanelOn
        {
            get => stagesUIView.detailPanelOn;
            set => stagesUIView.detailPanelOn = value;
        }

        [HideInInspector] public bool moving = false;
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
            stagesUIView = StagesUIView.Instance;
            settingManager = SettingManager.Instance;

            currentStage = gameData.gameDataElements.currentStage;
            transform.position = stageObjects[currentStage].transform.position;
            camera.position = new Vector3(transform.position.x, transform.position.y, -10);
            
            foreach (var other in stageObjects)
            {
                floatingEffects.Add(other.transform.GetChild(0).gameObject);
            }
            
            stageObjects[currentStage].transform.localScale = new Vector3(0.4f, 0.4f, 1f);
            floatingEffects[currentStage].SetActive(true);
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
            else if (!settingManager.settingPanelOn)
            {
                if (stagesUIView.escapePanelOn)
                {
                    var delta = Mathf.Abs(value.x) > Mathf.Abs(value.y) ? value.x : value.y;
                    if (Mathf.Approximately(delta, 0)) return;
                    var curr = (int)stagesUIView.escapeMenu;
                    var next = (curr + (delta > 0 ? -1 : 1) + stagesUIView.cnt) % stagesUIView.cnt;
                    stagesUIView.escapeMenu = (EscapeMenu)next;
                }
                else
                {
                    if (moving) return;
                    
                    var node = stageMap.nodes[currentStage];

                    var way = currentStage;
                    if (!Mathf.Approximately(value.x, 0))
                    {
                        way = int.Parse(value.x > 0 ? node.right : node.left);
                    }

                    if (!Mathf.Approximately(value.y, 0))
                    {
                        way = int.Parse(value.y > 0 ? node.up : node.down);
                    }
                    
                    if (currentStage == way) return;
                    
                    var previousStage = currentStage;
                    currentStage = way;
                    
                    if (stageObjects[currentStage].transform.position.x > transform.position.x)
                    {
                        transform.rotation = Quaternion.Euler(0, 180, 0);
                    }
                    else if (stageObjects[currentStage].transform.position.x < transform.position.x)
                    {
                        transform.rotation = Quaternion.Euler(0, 0, 0);
                    }
                    transform.DOMove(stageObjects[currentStage].transform.position, duration);
                    
                    moving = true;
                    camera.DOMove(
                        new Vector3(stageObjects[currentStage].transform.position.x,
                            stageObjects[currentStage].transform.position.y,
                            -10), 1f).OnComplete(
                        () =>
                        {
                            stageObjects[previousStage].transform.localScale = new Vector3(0.3f, 0.3f, 1f);
                            floatingEffects[previousStage].SetActive(false);
                            
                            stageObjects[currentStage].transform.localScale = new Vector3(0.4f, 0.4f, 1f);
                            floatingEffects[currentStage].SetActive(true);
                            
                            moving = false;
                        });
                }
            }
        }

        public void OnSubmitPerformed(InputAction.CallbackContext ctx)
        {
            if (moving) return;

            if (stagesUIView.escapePanelOn)
            {
                IMenuSubmittable stagesFinish = StagesFinish.Instance;
                stagesFinish.Submit();
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
                    OnDestroy();
                    GameDataContainer.currentStage = stageMap.nodes[currentStage];
                    CircularEffect.Instance.CircularOut(() =>
                    {
                        SceneManager.LoadScene("InGame");
                    });
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
            settingManager.Off();
            if (detailPanelOn)
            {
                Escape();
            }
            else if (stagesUIView.escapePanelOn)
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