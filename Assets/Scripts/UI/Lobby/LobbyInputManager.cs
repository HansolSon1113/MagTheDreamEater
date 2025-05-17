using System.Collections.Generic;
using DG.Tweening;
using Interfaces;
using Setting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace UI.Lobby
{
    public interface IMenuContainer
    {
        Menu menu { get; set; }
    }

    public class LobbyInputManager : MonoBehaviour, IMovable, ISubmittable, IPointerClickHandler, IMenuContainer
    {
        private const int cnt = 4;
        private InputActionMap menuMap;
        public InputAction moveAction { get; set; }
        public InputAction submitAction { get; set; }
        private Menu _menu = Menu.NewStart;
        public LobbyFinish lobbyFinish;
        public static LobbyInputManager Instance;
        [SerializeField] private List<Transform> menuTransforms = new List<Transform>();
        [SerializeField] private float duration;
        private SettingManager settingManager;

        public Menu menu
        {
            get => _menu;
            set
            {
                _menu = value;
                AnimateTo(_menu);
            }
        }

        public void Awake()
        {
            Instance = this;
        }

        public void Assign()
        {
            menuMap = InputSystem.actions.FindActionMap("Lobby");
            menuMap.Enable();

            moveAction = InputSystem.actions.FindAction("Move");
            moveAction.performed += OnMovePerformed;
            moveAction.Enable();
            submitAction = InputSystem.actions.FindAction("Submit");
            submitAction.performed += OnSubmitPerformed;
            submitAction.Enable();
        }

        private void Start()
        {
            lobbyFinish = LobbyFinish.Instance;
            settingManager = SettingManager.Instance;
        }

        public void OnDestroy()
        {
            moveAction.performed -= OnMovePerformed;
            submitAction.performed -= OnSubmitPerformed;

            moveAction.Disable();
            submitAction.Disable();

            menuMap.Disable();
        }

        public void OnMovePerformed(InputAction.CallbackContext ctx)
        {
            var value = ctx.ReadValue<Vector2>();
            Move(value);
        }

        public void Move(Vector2 value)
        {
            if ((Mathf.Approximately(value.x, 0f) && Mathf.Approximately(value.y, 0)) || settingManager.settingPanelOn) return;

            var curr = (int)_menu;
            var next = (curr + (value.x > 0 || value.y > 0 ? -1 : 1) + cnt) % cnt;
            menu = (Menu)next;

            AnimateTo(_menu);
        }

        public void OnSubmitPerformed(InputAction.CallbackContext ctx)
        {
            Submit();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            lobbyFinish.Finish(menu);
        }

        public void Submit()
        {
            if (settingManager.settingPanelOn) return;
            lobbyFinish.Finish(_menu);
        }

        private void AnimateTo(Menu value)
        {
            transform.DOMove(menuTransforms[(int)value].position, duration);
        }
    }
}