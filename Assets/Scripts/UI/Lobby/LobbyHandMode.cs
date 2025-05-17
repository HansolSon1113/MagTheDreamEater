using System.Collections.Generic;
using Interfaces;
using SaveData;
using UI.Effects;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace UI.Lobby
{
    public class LobbyHandMode: MonoBehaviour, IMovable, ISubmittable
    {
        public static LobbyHandMode Instance;
        public InputAction moveAction { get; set; }
        public InputAction submitAction { get; set; }
        [HideInInspector] public HandMode handMode;
        public List<GameObject> overlayObjects;
        
        public void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            LobbyInputManager.Instance.OnDestroy();
            
            moveAction = InputSystem.actions.FindAction("Move");
            moveAction.performed += OnMovePerformed;
            moveAction.Enable();
            submitAction = InputSystem.actions.FindAction("Submit");
            submitAction.performed += OnSubmitPerformed;
            submitAction.Enable();
            
            overlayObjects[(int)handMode].SetActive(true);
        }

        public void OnDestroy()
        {
            GameDataContainer.settingData.settingDataElements.handMode = handMode;
            
            moveAction.performed -= OnMovePerformed;
            moveAction.Disable();
            submitAction.performed -= OnSubmitPerformed;
            submitAction.Disable();
        }
        
        public void OnSubmitPerformed(InputAction.CallbackContext ctx)
        {
            Submit();
        }

        public void Submit()
        {
            GameDataContainer.settingData.settingDataElements.handMode = handMode;
            UIFadeEffect.Instance.FadeOut(() => { SceneManager.LoadScene("Stages"); });
        }

        public void OnMovePerformed(InputAction.CallbackContext ctx)
        {
            var value = ctx.ReadValue<Vector2>();
            
            Move(value);
        }

        public void Move(Vector2 value)
        {
            if (Mathf.Approximately(value.x, 0) && Mathf.Approximately(value.y, 0)) return;
            
            handMode = (HandMode)(((int)handMode + (value.x > 0 || value.y > 0 ? 1 : -1) + 2) % 2);
            HighLight();
        }
        
        private void HighLight()
        {
            overlayObjects[0].SetActive(false);
            overlayObjects[1].SetActive(false);
                            
            overlayObjects[(int)handMode].SetActive(true);
        }
    }
}