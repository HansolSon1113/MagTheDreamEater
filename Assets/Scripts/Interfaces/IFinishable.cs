using Setting;
using UI.Lobby;
using UI.Stages;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Interfaces
{
    public interface IFinishable: IMenuSubmittable
    {
        IEscapePanel view { get; set; }
        static SceneFinish Instance { get; }

        abstract void Start();
    }

    public abstract class SceneFinish : MonoBehaviour, IFinishable, IPointerClickHandler
    {
        public IEscapePanel view { get; set; }
        public static SceneFinish Instance;
        private SettingManager settingManager;

        public void Awake()
        {
            Instance = this;
        }

        public virtual void Start()
        {
            settingManager = SettingManager.Instance;
        }

        public void Submit()
        {
            Time.timeScale = 1f;

            switch (view.escapeMenu)
            {
                case EscapeMenu.Resume:
                    view.escapePanelOn = false;
                    break;
                case EscapeMenu.Settings:
                    settingManager.On();
                    break;
                case EscapeMenu.Lobby:
                    Do();
                    break;
                case EscapeMenu.Exit:
                    Application.Quit();
                    break;
                default:
                    Debug.LogError("Invalid stageEscape!");
                    break;
            }
        }

        protected abstract void Do();
        
        public void OnPointerClick(PointerEventData eventData)
        {
            Submit();
        }
    }
}