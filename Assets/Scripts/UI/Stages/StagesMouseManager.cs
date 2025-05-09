using UI.Effects;
using UnityEngine;

namespace UI.Stages
{
    public class StagesMouseManager : UIOverlayEffect
    { 
        private IStageMenuContainer stagesUIView;
        [SerializeField] private StageMenu stageMenu;
        [SerializeField] private StageEscape stageEscape;

        private void Start()
        {
            stagesUIView = StagesUIView.Instance;
        }

        public override void Do()
        {
            if(stagesUIView.detailPanelOn)
                stagesUIView.stageMenu = stageMenu;
            else if (stagesUIView.escapePanelOn)
            {
                stagesUIView.stageEscape = stageEscape;
            }
        }
    }
}