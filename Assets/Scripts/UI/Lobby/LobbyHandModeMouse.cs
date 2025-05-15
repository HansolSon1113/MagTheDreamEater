using System;
using SaveData;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Lobby
{
    public class LobbyHandModeMouse: MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] private HandMode handMode;
        private GameObject overlay;
        private LobbyHandMode lobbyHandMode;

        private void Start()
        {
            lobbyHandMode = LobbyHandMode.Instance;
            
            overlay = lobbyHandMode.overlayObjects[(int)handMode];
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            lobbyHandMode.handMode = handMode;
            lobbyHandMode.Submit();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            HighLight();
        }

        private void HighLight()
        {
            overlay.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if(lobbyHandMode.handMode != handMode)
                overlay.SetActive(false);
        }
    }
}