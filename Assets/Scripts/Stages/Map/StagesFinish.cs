using Interfaces;
using UI.Stages;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Stages.Map
{
    public class StagesFinish : SceneFinish
    {
        public override void Start()
        {
            view = StagesUIView.Instance;
        }

        public override void Submit()
        {
            Time.timeScale = 1;
            
            switch (view.stageEscape)
            {
                case StageEscape.Resume:
                    view.escapePanelOn = false;
                    break;
                case StageEscape.Settings:
                    break;
                case StageEscape.Lobby:
                    SceneManager.LoadScene("Lobby");
                    break;
                case StageEscape.Exit:
                    Application.Quit();
                    break;
                default:
                    Debug.LogError("Invalid stageEscape!");
                    break;
            }
        }
    }
}