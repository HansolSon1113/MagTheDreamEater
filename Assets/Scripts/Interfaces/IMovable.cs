using UnityEngine.InputSystem;

namespace Interfaces
{
    public interface IMovable
    {
        public InputAction moveAction { get; set; }

        void Start();
    
        void OnDestroy();
    
        void OnMovePerformed(InputAction.CallbackContext ctx);
    }
}