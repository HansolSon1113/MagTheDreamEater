using UI.Effects;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Lobby
{
    public class LobbyMouseManager : UIOverlayEffect
    {
        private IMenuContainer lobbyManager;
        [SerializeField] private Menu menu;
        private IMenuSubmittable lobbyFinish;

        private void Start()
        {
            lobbyManager = LobbyInputManager.Instance;
            lobbyFinish = LobbyFinish.Instance;
        }
        
        protected override void Do()
        {
            lobbyManager.menu = menu;
        }
        

    }
}
