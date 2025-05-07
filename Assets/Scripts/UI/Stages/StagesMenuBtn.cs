using Interfaces;
using Stages.Map;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Stages
{
    public class StagesMenuBtn: MonoBehaviour, IPointerClickHandler
    {
        private StagesManager stagesManager;
        private IShowContainer stagesUIView;
        
        private void Start()
        {
            stagesManager = StagesManager.Instance;
            stagesUIView = StagesUIView.Instance;
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (stagesManager.detailPanelOn)
            {
                stagesManager.Submit();
            }
            else
            {
                stagesUIView.Show();
                stagesManager.detailPanelOn = true;
            }
        }
    }
}