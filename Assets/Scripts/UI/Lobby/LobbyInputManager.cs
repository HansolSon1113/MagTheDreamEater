using System.Collections.Generic;
using DG.Tweening;
using Interfaces;
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
            var value = ctx.ReadValue<float>();
            Move(new Vector2(value, 0));
        }

        public void Move(Vector2 value)
        {
            var delta = value.x;
            if (Mathf.Approximately(delta, 0f)) return;

            var curr = (int)_menu;
            var next = (curr + (delta > 0 ? 1 : -1) + cnt) % cnt;
            menu = (Menu)next;

            AnimateTo(_menu);
        }

        public void OnSubmitPerformed(InputAction.CallbackContext ctx)
        {
            Submit();
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            lobbyFinish.Submit(menu);
        }

        public void Submit()
        {
            lobbyFinish.Submit(_menu);
        }

        private void AnimateTo(Menu value)
        {
            transform.DOMove(menuTransforms[(int)value].position, duration);
        }
    }
}