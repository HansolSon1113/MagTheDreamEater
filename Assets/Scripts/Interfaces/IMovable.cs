using UnityEngine;
using UnityEngine.InputSystem;

namespace Interfaces
{
    public interface IMovable
    {
        public InputAction moveAction { get; set; }

        void Awake();
    
        void OnDestroy();
    
        void OnMovePerformed(InputAction.CallbackContext ctx);

        void Move(Vector2 value);
    }
}