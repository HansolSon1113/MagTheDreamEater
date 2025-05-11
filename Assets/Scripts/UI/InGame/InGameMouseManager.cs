using UI.Effects;
using UI.Stages;
using UnityEngine;

namespace UI.InGame
{
    public class InGameMouseManager : UIOverlayEffect
    {
        private InGameUIView inGameUIView;
        [SerializeField] private EscapeMenu escapeMenu;

        private void Start()
        {
            inGameUIView = InGameUIView.Instance;
        }

        public override void Do()
        {
            inGameUIView.escapeMenu = escapeMenu;
        }
    }
}