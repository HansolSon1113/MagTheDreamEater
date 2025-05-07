using Interfaces;
using Stages.Map;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Stages
{
    public class StagesMouseManager : MonoBehaviour, IPointerEnterHandler
    { 
        private IStageMenuContainer stagesUIView;
        [SerializeField] private StageMenu stageMenu;

        private void Start()
        {
            stagesUIView = StagesUIView.Instance;
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            stagesUIView.stageMenu = stageMenu;
        }
    }
}