using Interfaces;
using Setting;
using UI.InGame;
using UI.Lobby;
using UI.Stages;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InGame.Managers
{
    public class InGameManager : MonoBehaviour, IMovable, ISubmittable, IEscapable
    {
        private IEscapePanel inGameUIView;
        private InputActionMap inGameActionMap;
        public InputAction moveAction { get; set; }
        public InputAction submitAction { get; set; }
        public InputAction escapeAction { get; set; }
        private SettingManager settingManager;

        public void Awake()
        {
            inGameActionMap = InputSystem.actions.FindActionMap("InGame");
            inGameActionMap.Enable();

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
            inGameUIView = InGameUIView.Instance;
            settingManager = SettingManager.Instance;
        }

        public void OnDestroy()
        {
            moveAction.performed -= OnMovePerformed;
            submitAction.performed -= OnSubmitPerformed;
            escapeAction.performed -= OnEscapePerformed;
            moveAction.Disable();
            submitAction.Disable();
            escapeAction.Disable();

            inGameActionMap.Disable();
        }

        public void OnMovePerformed(InputAction.CallbackContext ctx)
        {
            if (!inGameUIView.escapePanelOn || settingManager.settingPanelOn) return;
            var value = ctx.ReadValue<Vector2>();
            Move(value);
        }

        public void Move(Vector2 value)
        {
            var delta = Mathf.Abs(value.x) > Mathf.Abs(value.y) ? value.x : value.y;
            if (Mathf.Approximately(delta, 0)) return;
            var curr = (int)inGameUIView.escapeMenu;
            var next = (curr + (delta > 0 ? -1 : 1) + inGameUIView.cnt) % inGameUIView.cnt;
            inGameUIView.escapeMenu = (EscapeMenu)next;
        }

        public void OnSubmitPerformed(InputAction.CallbackContext ctx)
        {
            Submit();
        }

        public void Submit()
        {
            if (inGameUIView.escapePanelOn)
            {
                IMenuSubmittable inGameFinish = InGameFinish.Instance;
                inGameFinish.Submit();
            }
        }

        public void OnEscapePerformed(InputAction.CallbackContext ctx)
        {
            Escape();
        }

        public void Escape()
        {
            settingManager.Off();
            inGameUIView.escapePanelOn = !inGameUIView.escapePanelOn;
        }
    }
}