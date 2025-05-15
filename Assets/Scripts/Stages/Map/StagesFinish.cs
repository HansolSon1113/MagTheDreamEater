using Interfaces;
using UI.Effects;
using UI.Stages;
using UnityEngine.SceneManagement;

namespace Stages.Map
{
    public class StagesFinish : SceneFinish
    {
        public override void Start()
        {
            base.Start();
            view = StagesUIView.Instance;
        }

        protected override void Do()
        {
            CircularEffect.Instance.CircularOut(() =>
            {
                SceneManager.LoadScene("Lobby");
            });
        }
    }
}