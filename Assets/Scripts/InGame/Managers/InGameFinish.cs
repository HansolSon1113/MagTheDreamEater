using Interfaces;
using UI.InGame;
using UI.Stages;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace InGame.Managers
{
    public class InGameFinish : SceneFinish
    {
        public override void Start()
        {
            view = InGameUIView.Instance;
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
                    SceneManager.LoadScene("Stages");
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