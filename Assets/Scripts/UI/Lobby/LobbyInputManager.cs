using System.Collections.Generic;
using DG.Tweening;
using Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace UI.Lobby
{
    public class LobbyInputManager : MonoBehaviour, IPointerClickHandler, IMovable, ISubmittable
    {
        private const int cnt = 4;
        private InputActionMap menuMap;
        public InputAction moveAction { get; set; }
        public InputAction submitAction { get; set; }
        private Menu _menu = Menu.NewStart;
        private LobbyFinish lobbyFinish;
        [SerializeField] private List<Transform> menuTransforms = new List<Transform>();
        [SerializeField] private float duration;

        public Menu menu
        {
            get => _menu;
            set
            {
                _menu = value;
                AnimateTo(_menu);
            }
        }

        public void Start()
        {
            menuMap = InputSystem.actions.FindActionMap("Lobby");
            menuMap.Enable();

            moveAction = InputSystem.actions.FindAction("Move");
            moveAction.performed += OnMovePerformed;
            moveAction.Enable();
            submitAction = InputSystem.actions.FindAction("Submit");
            submitAction.performed += OnSubmitPerformed;
            submitAction.Enable();

            lobbyFinish = LobbyFinish.Instance;
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
            var delta = ctx.ReadValue<float>();
            if (Mathf.Approximately(delta, 0f)) return;

            var curr = (int)menu;
            var next = (curr + (delta > 0 ? 1 : -1) + cnt) % cnt;
            menu = (Menu)next;

            AnimateTo(menu);
        }

        public void OnSubmitPerformed(InputAction.CallbackContext ctx)
        {
            lobbyFinish.Submit(menu);
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            lobbyFinish.Submit(menu);
        }

        private void AnimateTo(Menu value)
        {
            transform.DOMove(menuTransforms[(int)value].position, duration);
        }
    }
}