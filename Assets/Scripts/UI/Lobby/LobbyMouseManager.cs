using UI.Effects;
using UnityEngine;

namespace UI.Lobby
{
    public class LobbyMouseManager : UIOverlayEffect
    {
        [SerializeField] private LobbyInputManager lobbyManager;
        [SerializeField] private Menu menu;

        protected override void Do()
        {
            lobbyManager.menu = menu;
        }
    }
}
