using UI.Effects;

namespace UI.Lobby
{
    public class LobbyFadeEffect: UIFadeEffect
    {
        protected override void Do()
        {
            LobbyInputManager.Instance.Assign();
        }
    }
}