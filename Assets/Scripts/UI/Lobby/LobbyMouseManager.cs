using DG.Tweening;
using UI.Effects;
using UnityEngine;

namespace UI.Lobby
{
    public class LobbyMouseManager : UIOverlayEffect
    {
        private IMenuContainer lobbyManager;
        [SerializeField] private Menu menu;

        private void Start()
        {
            lobbyManager = LobbyInputManager.Instance;
        }
        
        public override void Do()
        {
            rect.DOMove(transform.position, duration);
            lobbyManager.menu = menu;
        }
    }
}
