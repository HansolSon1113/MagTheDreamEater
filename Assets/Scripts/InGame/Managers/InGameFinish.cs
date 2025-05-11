using Interfaces;
using UI.InGame;
using UnityEngine.SceneManagement;

namespace InGame.Managers
{
    public class InGameFinish : SceneFinish
    {
        public override void Start()
        {
            base.Start();
            view = InGameUIView.Instance;
        }

        protected override void Do()
        {
            SceneManager.LoadScene("Stages");
        }
    }
}