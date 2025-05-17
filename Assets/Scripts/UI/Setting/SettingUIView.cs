using System.Collections.Generic;
using SaveData;
using Setting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using Interfaces;
using UnityEngine.EventSystems;

namespace UI.Setting
{
    public class SettingUIView : MonoBehaviour, IMovable, IEscapable
    {
        [SerializeField] private Slider totalVolumeSlider, popVolumeSlider, musicVolumeSlider;
        [SerializeField] private TMP_InputField totalVolumeField, popVolumeField, musicVolumeField;
        public List<GameObject> overlays = new List<GameObject>();
        private SettingManager settingManager;
        public InputAction moveAction { get; set; }
        private int currentFieldIndex = 0;
        private TMP_InputField[] inputFields;

        public float totalVolume
        {
            set
            {
                settingManager.totalVolume = totalVolumeSlider.value = value;
                totalVolumeField.text = (settingManager.totalVolume * 100).ToString();
            }
        }

        public float popVolume
        {
            set
            {
                settingManager.popVolume = popVolumeSlider.value = value;
                popVolumeField.text = (settingManager.popVolume * 100).ToString();
            }
        }

        public float musicVolume
        {
            set
            {
                settingManager.musicVolume = musicVolumeSlider.value = value;
                musicVolumeField.text = (settingManager.musicVolume * 100).ToString();
            }
        }

        public HandMode handMode
        {
            set
            {
                overlays[(int)settingManager.handMode].SetActive(false);

                settingManager.handMode = value;
                overlays[(int)settingManager.handMode].SetActive(true);
            }
        }

        public InputAction escapeAction { get; set; }

        public void Awake()
        {
            inputFields = new[] { totalVolumeField, popVolumeField, musicVolumeField };
        }

        private void Start()
        {
            settingManager = SettingManager.Instance;
            var settingData = settingManager.settingData.settingDataElements;

            totalVolume = settingData.totalVolume;
            popVolume = settingData.popVolume;
            musicVolume = settingData.musicVolume;

            totalVolumeSlider.onValueChanged.AddListener((value) => { totalVolume = value; });
            popVolumeSlider.onValueChanged.AddListener((value) => { popVolume = value; });
            musicVolumeSlider.onValueChanged.AddListener((value) => { musicVolume = value; });

            totalVolumeField.onEndEdit.AddListener((value) => { totalVolume = float.Parse(value) / 100 % 100; });
            popVolumeField.onEndEdit.AddListener((value) => { popVolume = float.Parse(value) / 100 % 100; });
            musicVolumeField.onEndEdit.AddListener((value) => { musicVolume = float.Parse(value) / 100 % 100; });

            overlays[(int)settingManager.handMode].SetActive(true);
        }

        private void OnEnable()
        {
            moveAction = InputSystem.actions.FindAction("Move");
            moveAction.performed += OnMovePerformed;
            moveAction.Enable();

            escapeAction = InputSystem.actions.FindAction("Escape");
            escapeAction.performed += OnEscapePerformed;
            escapeAction.Enable();

            SelectField(0);
        }

        public void OnDisable()
        {
            moveAction.performed -= OnMovePerformed;
            escapeAction.performed -= OnEscapePerformed;

            UnselectCurrentField();
        }

        public void OnDestroy()
        {
        }

        public void OnEscapePerformed(InputAction.CallbackContext ctx)
        {
            Escape();
        }

        public void Escape()
        {
            UnselectCurrentField();
            settingManager.Off();
        }

        public void OnMovePerformed(InputAction.CallbackContext ctx)
        {
            Move(ctx.ReadValue<Vector2>());
        }

        public void Move(Vector2 value)
        {
            if (Mathf.Approximately(value.x, 0) && Mathf.Approximately(value.y, 0)) return;

            var newIndex = currentFieldIndex;

            if (Mathf.Abs(value.y) > 0)
            {
                var direction = value.y > 0 ? -1 : 1;
                newIndex = (currentFieldIndex + direction + inputFields.Length) % inputFields.Length;
            }
            else if (Mathf.Abs(value.x) > 0)
            {
                handMode = (HandMode)(((int)settingManager.handMode + (value.x > 0 ? 1 : -1) + 2) % 2);
            }

            if (newIndex != currentFieldIndex)
            {
                SelectField(newIndex);
            }
        }

        private void SelectField(int index)
        {
            if (currentFieldIndex >= 0 && currentFieldIndex < inputFields.Length)
            {
                inputFields[currentFieldIndex].DeactivateInputField();
            }

            currentFieldIndex = index;
            inputFields[currentFieldIndex].ActivateInputField();
            inputFields[currentFieldIndex].Select();
        }

        private void UnselectCurrentField()
        {
            if (currentFieldIndex < 0 || currentFieldIndex >= inputFields.Length) return;
            foreach (var field in inputFields)
            {
                field.DeactivateInputField();
            }

            EventSystem.current.SetSelectedGameObject(null);
        }
    }
}